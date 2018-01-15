using PredictionApp.Common;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    public class StockManager
    {
        /// <summary>
        /// a service to manage restaurant operations on datasource 
        /// </summary>
        RestaurantService _restaurantService;

        /// <summary>
        /// a service to manage product operations on datasource 
        /// </summary>
        ProductService _productService;

        /// <summary>
        /// a service to manage staff operations on datasource 
        /// </summary>
        StaffService _staffService;

        /// <summary>
        /// a service to manage supply operations on datasource 
        /// </summary>
        SupplyService _supplyService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="restaurantService">restaurantService</param>
        /// <param name="productService">productService</param>
        /// <param name="staffService">staffService</param>
        /// <param name="supplyService">supplyService</param>
        public StockManager(RestaurantService restaurantService, ProductService productService, StaffService staffService, SupplyService supplyService)
        {
            _restaurantService = restaurantService;
            _productService = productService;
            _staffService = staffService;
            _supplyService = supplyService;
        }

        /// <summary>
        /// Initialize restaurants start-up stock
        /// </summary>
        /// <param name="date">supplyDate</param>
        public void InitializeStock(DateTime date)
        {
            //Get all mappings and expected stock amounts
            var restaurantProductMappings = _supplyService.GetRestaurantProductMappings(new GetRestaurantProductMappingRequest()).RestaurantProductMappings;
            var mappingCount = restaurantProductMappings.Count;

            //Give order 
            Parallel.ForEach(restaurantProductMappings, mapping =>
            {
                //Get
                var responsibleStaff = GetResponsibleStaff(mapping.RestaurantID);
                if (responsibleStaff == null)
                {
                    //LOG
                    return;
                }

                //order the initialize products
                var orderId = _supplyService.GiveOrder(BuildGiveOrderRequest(mapping.RestaurantID, responsibleStaff.ID, mapping.ProductID, mapping.ExpectedStockAmount, date)).OrderID;

                //Given orders are provided by supplier.
                _supplyService.Provide(new ProvideProductOrderRequest
                {
                    OrderID = orderId,
                    UnitsInStock = 0,
                    ExpirationDate = date.AddDays(RandomHelper.RandomInteger(Constants.ProductExpirationDayRange.Min, Constants.ProductExpirationDayRange.Max)),
                    ReceivedDateTime = date
                });
            });
        }

        /// <summary>
        /// Create product start-up stock mappings for all restaurants
        /// </summary>
        public void SetDefaultStockList()
        {
            //Get all restaurants from data source
            var restaurants = _restaurantService.Get(new GetRestaurantRequest()).Restaurants;
            var restaurantCount = restaurants.Count;

            //Get all products from data source
            var products = _productService.Get(new GetProductsRequest()).Products;
            var productsCount = products.Count;

            //For each restaurant
            Parallel.ForEach(restaurants, restaurant =>
            {
                //For each products
                Parallel.ForEach(products, product =>
                {
                    _supplyService.CreateRestaurantProductMappings(new CreateRestaurantProductMappingRequest
                    {
                        RestaurantID = restaurant.ID,
                        ProductID = product.ID,
                        ExpectedStockAmount = RandomHelper.RandomDouble(Constants.ExpectedStockAmount.Min, Constants.ExpectedStockAmount.Max)
                    });
                });
            });
        }

        /// <summary>
        /// Checks supply required restaurants and supply required products
        /// </summary>
        /// <param name="date">supplyDate</param>
        public void Supply(DateTime date)
        {
            var supplyRequiredRestaurants = _restaurantService.GetBySupplyDayOfWeek(new GetRestaurantsBySupplyDayOfWeekRequest { SupplyDayOfWeek = (byte)date.DayOfWeek }).Restaurants;
            var supplyRequiredRestaurantCount = supplyRequiredRestaurants.Count;

            //If today isn't a supply day for any restaurant
            if (supplyRequiredRestaurantCount == default(int))
                return;

            foreach (var restaurant in supplyRequiredRestaurants)
            {  //Parallel.ForEach(supplyRequiredRestaurants, restaurant =>
                //{
                //Get sales staff for current restaurant
                var responsibleStaff = GetResponsibleStaff(restaurant.ID);
                if (responsibleStaff == null)
                {
                    //LOG
                    return;
                }

                //Get Latest stock transactions 
                var latestStockTransactionLookup = _productService.GetLatestStockTransactions(new GetLatestStockTransactionsRequest { RestaurantId = restaurant.ID, StartDate = date.Date.AddDays(-7) }).LastStockTransactions.ToLookup(x => x.ProductID);

                //Get products that have stock transaction for current restaurant
                var productIdList = latestStockTransactionLookup.Select(x => x.Key);

                foreach (var productId in productIdList)
                {
                    //Get latest transactions for current restaurant - product pair.
                    var latestProductTransactions = latestStockTransactionLookup[productId];

                    //Get Remaining Amount
                    var lastTransaction = latestProductTransactions.OrderBy(x => x.CreatedDatetime).LastOrDefault();
                    if (lastTransaction == null)
                    {
                        // There is no transaction for this seven days. So, no order is reqired.
                    }

                    //Get consumed amount for current restaurant - product pair.
                    var consumedAmount = latestProductTransactions.Where(x => x.TransactionAmount < 0).Sum(x => x.TransactionAmount);

                    //Check if stock is enough for next week
                    var requiredAmount = Math.Abs(consumedAmount) - lastTransaction.RemainingAmount;
                    if (requiredAmount > 0)
                    {
                        //Give an order for this product
                        var giveOrderResponse = _supplyService.GiveOrder(BuildGiveOrderRequest(restaurant.ID, responsibleStaff.ID, productId, requiredAmount, date));

                        //Given orders are provided by supplier.
                        _supplyService.Provide(new ProvideProductOrderRequest
                        {
                            OrderID = giveOrderResponse.OrderID,
                            UnitsInStock = lastTransaction.RemainingAmount,
                            ExpirationDate = date.AddDays(RandomHelper.RandomInteger(Constants.ProductExpirationDayRange.Min, Constants.ProductExpirationDayRange.Max)),
                            ReceivedDateTime = date
                        });
                    }
                }


                //Parallel.ForEach(productIdList, productId =>
                //   {
                //       //Get latest transactions for current restaurant - product pair.
                //       var latestProductTransactions = latestStockTransactionLookup[productId];

                //       //Get Remaining Amount
                //       var lastTransaction = latestProductTransactions.OrderBy(x => x.CreatedDatetime).LastOrDefault();
                //       if (lastTransaction == null)
                //       {
                //           // There is no transaction for this seven days. So, no order is reqired.
                //       }

                //       //Get consumed amount for current restaurant - product pair.
                //       var consumedAmount = latestProductTransactions.Where(x => x.TransactionAmount < 0).Sum(x => x.TransactionAmount);

                //       //Check if stock is enough for next week
                //       var requiredAmount = consumedAmount - lastTransaction.RemainingAmount;
                //       if (requiredAmount > 0)
                //       {
                //           //Give an order for this product
                //           var giveOrderResponse = _supplyService.GiveOrder(BuildGiveOrderRequest(restaurant.ID, responsibleStaff.ID, productId, requiredAmount));

                //           //Given orders are provided by supplier.
                //           _supplyService.Provide(new ProvideProductOrderRequest
                //           {
                //               OrderID = giveOrderResponse.OrderID,
                //               UnitsInStock = lastTransaction.RemainingAmount,
                //               ExpirationDate = date.AddDays(RandomHelper.RandomInteger(Constants.ProductExpirationDayRange.Min, Constants.ProductExpirationDayRange.Max))
                //           });
                //       }
                //   });
                //  });
            }
        }

        /// <summary>
        /// Builds an order product request by given parameters
        /// </summary>
        /// <param name="restaurantId">for which restaurantId</param>
        /// <param name="responsibleStaffId">for which responsibleStaffId</param>
        /// <param name="productId">for which productId</param>
        /// <param name="orderAmount">for which orderAmount</param>
        /// <param name="orderDate">for which orderDate</param>
        /// <returns>returns an instance of GiveProductOrderRequest</returns>
        private GiveProductOrderRequest BuildGiveOrderRequest(Guid restaurantId, Guid responsibleStaffId, Guid productId, double orderAmount, DateTime orderDate)
        {
            GiveProductOrderDTO order = new GiveProductOrderDTO
            {
                RestaurantID = restaurantId,
                StaffID = responsibleStaffId,
                ProductID = productId,
                Amount = orderAmount,
                OrderDate = orderDate
            };

            return new GiveProductOrderRequest { Order = order };
        }

        /// <summary>
        /// Gives a staff randomly
        /// </summary>
        /// <param name="restaurantId">from which restaurant</param>
        /// <returns>randomly selected staffDto</returns>
        private StaffDTO GetResponsibleStaff(Guid restaurantId)
        {
            //Get all staffs (workplaceid can be specified as parameter)
            var staffs = _staffService.Get(new GetStaffRequest { }).Staffs;

            //If there is no staff, return null
            if (staffs == null)
            {
                return null;
            }

            //return random staff
            return staffs[RandomHelper.RandomInteger(0, staffs.Count)];
        }
    }
}

using PredictionApp.Repository;
using System;
using System.Linq;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with supply transactions
    /// </summary>
    public class SupplyService : ServiceBase
    {
        /// <summary>
        /// Repository for supply transaction table
        /// </summary>
        private SupplyTransactionRepository _supplyTransactionRepository;

        /// <summary>
        /// Repository for restaurant-product mapping table
        /// </summary>
        private RestaurantProductMappingRepository _restaurantProductMappingRepository;

        /// <summary>
        /// Repository for product stock transaction table
        /// </summary>
        private ProductStockTransactionRepository _productStockTransactionRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public SupplyService(DbSettings dbSettings) : base(dbSettings)
        {
            _supplyTransactionRepository = new SupplyTransactionRepository(_dbSettings.ConnectionString);
            _restaurantProductMappingRepository = new RestaurantProductMappingRepository(_dbSettings.ConnectionString);
            _productStockTransactionRepository = new ProductStockTransactionRepository(_dbSettings.ConnectionString);
        }

        /// <summary>
        /// Create restaurant product start-up stock mappings
        /// </summary>
        /// <param name="request">request to create restaurant product start-up stock mappings</param>
        /// <returns>returns response of the operation</returns>
        public CreateRestaurantProductMappingResponse CreateRestaurantProductMappings(CreateRestaurantProductMappingRequest request)
        {
            //Map request to entity
            var entity = new RestaurantProductMappingEntity
            {
                ProductID = request.ProductID,
                RestaurantID = request.RestaurantID,
                ExpectedStockAmount = request.ExpectedStockAmount
            };

            //Add mapping record into database
            _restaurantProductMappingRepository.Add(entity);

            return new CreateRestaurantProductMappingResponse();
        }

        /// <summary>
        /// Get restaurant product start-up stock mappings
        /// </summary>
        /// <param name="request">request to take restaurant product start-up stock mappings</param>
        /// <returns>returns response of the operation</returns>
        public GetRestaurantProductMappingResponse GetRestaurantProductMappings(GetRestaurantProductMappingRequest request)
        {
            //Map entity to response
            var mappings = _restaurantProductMappingRepository.Get().Select(mapping => new RestaurantProductMappingDTO
            {
                ProductID = mapping.ProductID,
                RestaurantID = mapping.RestaurantID,
                ExpectedStockAmount = mapping.ExpectedStockAmount
            }).ToList();

            //Return mappings
            return new GetRestaurantProductMappingResponse { RestaurantProductMappings = mappings };
        }

        /// <summary>
        /// Give an product order for a restaurant
        /// </summary>
        /// <param name="request">request to give a product order</param>
        /// <returns>returns response of the operation</returns>
        public GiveProductOrderResponse GiveOrder(GiveProductOrderRequest request)
        {
            //Map request to entity
            var orderEntity = new SupplyTransactionEntity
            {
                ID = Guid.NewGuid(),
                ProductID = request.Order.ProductID,
                RestaurantID = request.Order.RestaurantID,
                StaffID = request.Order.StaffID,
                Amount = request.Order.Amount,
                OrderDateTime = request.Order.OrderDate
            };

            //Add on supply transaction table
            _supplyTransactionRepository.Add(orderEntity);

            //Prepare response
            var response = new GiveProductOrderResponse();
            response.OrderID = orderEntity.ID;

            return response;
        }

        /// <summary>
        /// Product entry for ordered products by supplier
        /// </summary>
        /// <param name="request">request to provide an order</param>
        /// <returns>returns response of the operation</returns>
        public ProvideProductOrderResponse Provide(ProvideProductOrderRequest request)
        {
            //Create new db transaction
            using (var scope = base.CreateTransactionScope())
            {
                //Get current product order record
                var supplyTransactionEntity = _supplyTransactionRepository.Get(request.OrderID);

                //Set ExpirationDate and Received datetime for order transaction record 
                supplyTransactionEntity.ExpirationDate = request.ExpirationDate;
                supplyTransactionEntity.ReceivedDateTime = request.ReceivedDateTime;

                //Update record
                _supplyTransactionRepository.Update(supplyTransactionEntity);


                //Save this product entry on product transaction table
                _productStockTransactionRepository.Add(new ProductStockTransactionEntity
                {
                    ProductID = supplyTransactionEntity.ProductID,
                    RestaurantID = supplyTransactionEntity.RestaurantID,
                    TransactionAmount = supplyTransactionEntity.Amount,
                    RemainingAmount = supplyTransactionEntity.Amount + request.UnitsInStock,
                    CreatedDatetime = supplyTransactionEntity.ReceivedDateTime.Value
                });

                //Commit transaction
                scope.Complete();
            }

            return new ProvideProductOrderResponse();
        }
    }
}



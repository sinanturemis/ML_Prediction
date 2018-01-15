using PredictionApp.Common;
using PredictionApp.Common.Extension;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    /// <summary>
    /// Manages customer operations
    /// </summary>
    public class CustomerManager
    {
        #region Fields

        /// <summary>
        /// a service to manage customer operations on datasource 
        /// </summary>
        private CustomerService _customerService;

        /// <summary>
        ///  a service to manage food operations on datasource 
        /// </summary>
        private FoodService _foodService;

        /// <summary>
        ///  a service to manage restaurant operations on datasource 
        /// </summary>
        private RestaurantService _restaurantService;

        #endregion

        #region Constuctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerService">customerService</param>
        /// <param name="foodService">foodService</param>
        /// <param name="restaurantService">restaurantService</param>
        public CustomerManager(CustomerService customerService, FoodService foodService, RestaurantService restaurantService)
        {
            _customerService = customerService;
            _foodService = foodService;
            _restaurantService = restaurantService;
        }

        #endregion

        #region Managers

        /// <summary>
        /// Produces new customers and add them to data source
        /// </summary>
        /// <param name="count">how many item will be created</param>
        public void GenerateCustomers(int count)
        {
            //prepare create customer request
            var createCustomerRequest = new CreateCustomerRequest() { CustomersToCreate = PrepareCustomers(count) };

            //Add it to datasource
            _customerService.CreateCustomer(createCustomerRequest);
        }

        /// <summary>
        /// Generates daily visit data
        /// </summary>
        /// <param name="currentDate">visit date of customer</param>
        public void GenerateDailyVisits(DateTime currentDate)
        {
            //Prepare visitors and restaurants to choose
            var customers = _customerService.Get(new GetCustomerRequest()).Customers;
            var restaurants = _restaurantService.Get(new GetRestaurantRequest()).Restaurants;
            var menu = new List<FoodDTO>();

            //Choose candidates
            var candidateVisitors = ChooseVisitors(customers);
            var candidateRestaurant = ChooseRestaurant();

            //Make an reservation
            var reservationId = MakeReservation(candidateVisitors, candidateRestaurant, currentDate);

            //Visit the restaurant
            var visitId = Visit(reservationId, currentDate);

            //Give food orders
            var orderId = GiveFoodOrder(visitId, menu);

            //GetDeliveredFoods
            GetDeliveredFoods(orderId);

            Leave(currentDate, visitId);
        }

        /// <summary>
        /// Clusters customers
        /// </summary>
        public void ClusterByLocation()
        {
            //Get all customers
            var customers = _customerService.Get(new GetCustomerRequest()).Customers;

            //Get all restaurants
            var restaurants = _restaurantService.Get(new GetRestaurantRequest()).Restaurants;

            //Prepare clustering algorithm instance and set centroids
            KMeansClusteringAlgorithm clusteringAlgorithm = new KMeansClusteringAlgorithm();
            var centroids = restaurants.Select(x => new KMeansClusteringAlgorithm.Centroid
            {
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            //Run k-mean clustering algorithm
            clusteringAlgorithm.Run(customers, centroids);

            //Update customers' LocationGroupId in parallel tasks
            Parallel.ForEach(customers, currentCustomer =>
            {
                _customerService.Update(new UpdateCustomerRequest
                {
                    ID = currentCustomer.ID,
                    Age = currentCustomer.Age,
                    GenderType = currentCustomer.GenderType,
                    Latitude = currentCustomer.Latitude,
                    Longitude = currentCustomer.Longitude,
                    LocationGroupId = currentCustomer.LocationGroupId,
                    VisitCountInYear = currentCustomer.VisitCountInYear
                });
            });
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Prepares random generated customer list
        /// </summary>
        /// <param name="count">how many customers will be created</param>
        /// <returns></returns>
        private List<CustomerDTO> PrepareCustomers(int count)
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();
            for (int i = 0; i < count; i++)
            {
                customers.Add(GenerateCustomer());
            }

            return customers;
        }

        /// <summary>
        /// Returns random generated customerDto
        /// </summary>
        /// <returns>random generated customerDto</returns>
        private CustomerDTO GenerateCustomer()
        {
            return new CustomerDTO
            {
                ID = Guid.NewGuid(),
                Age = (byte)RandomHelper.RandomInteger(Constants.AgeRange.Min, Constants.AgeRange.Max),
                GenderType = RandomHelper.RandomBoolean(),
                Latitude = RandomHelper.RandomDouble(Constants.LatitudeRange.Min, Constants.LatitudeRange.Max).ToGPSFormat(),
                Longitude = RandomHelper.RandomDouble(Constants.LongitudeRange.Min, Constants.LongitudeRange.Max).ToGPSFormat(),
                VisitCountInYear = default(int)
            };
        }

        #region Write Them

        private void GetDeliveredFoods(Guid orderId)
        {
            throw new NotImplementedException();
        }

        private void Leave(DateTime date, Guid visitId)
        {
            var dateOut = date.AddMinutes(RandomHelper.RandomInteger(Constants.CustomerVisitDurationRange.Min, Constants.CustomerVisitDurationRange.Max));
            var leaveResponse = _customerService.Leave(new CreateLeaveTransactionRequest
            {
                DateOut = dateOut,
                VisitTransactionId = visitId
            });
        }

        private Guid GiveFoodOrder(Guid visitId, List<FoodDTO> foods)
        {
            throw new NotImplementedException();
        }

        private Guid Visit(object reservationId, DateTime currentDate)
        {
            throw new NotImplementedException();
        }

        private Guid MakeReservation(List<CustomerDTO> candidateVisitors, RestaurantDTO candidateRestaurant, DateTime currentDate)
        {
            throw new NotImplementedException();
        }

        private RestaurantDTO ChooseRestaurant()
        {
            throw new NotImplementedException();
        }

        private List<CustomerDTO> ChooseVisitors(List<CustomerDTO> customers)
        {
            throw new NotImplementedException();
        }

        #endregion 

        #endregion
    }
}

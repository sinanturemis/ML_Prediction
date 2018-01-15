using PredictionApp.Common;
using PredictionApp.Common.Extension;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    public class RestaurantManager
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        Settings _settings;

        /// <summary>
        /// a service to manage restaurant operations on datasource 
        /// </summary>
        RestaurantService _restaurantService;

        /// <summary>
        /// a service to manage staff operations on datasource 
        /// </summary>
        StaffService _staffService;

        /// <summary>
        /// a service to manage customer operations on datasource 
        /// </summary>
        CustomerService _customerService;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings">settings</param>
        /// <param name="restaurantService">restaurantService</param>
        /// <param name="staffService">staffService</param>
        /// <param name="customerService">customerService</param>
        public RestaurantManager(Settings settings, RestaurantService restaurantService, StaffService staffService, CustomerService customerService)
        {
            _settings = settings;
            _restaurantService = restaurantService;
            _staffService = staffService;
            _customerService = customerService;
        }

        #endregion Constructor

        #region Managers

        /// <summary>
        /// Produces new restaurants and add them to data source
        /// </summary>
        /// <param name="count">how many restaurants will be created</param>
        public void GenerateRestaurants(int count)
        {
            var request = BuildCreateRestaurantRequest(count);
            _restaurantService.CreateRestaurant(request);
        }

        /// <summary>
        /// Produces new staffs and add them to data source
        /// </summary>
        /// <param name="count">how many restaurants will be hired</param>
        public void HireStaff(int count)
        {
            CreateStaffRequest request = BuildCreateStaffRequest(count);

            var response = _staffService.CreateStaff(request);
            if (response == null)
                throw new Exception("");
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="currentDatetime"></param>
        ///// <returns></returns>
        //public List<Guid> CreateVisit(DateTime currentDatetime)
        //{
        //    //Initialize start-up values
        //    var customersToGo = new List<CustomerDTO>();
        //    var currentReservationID = Guid.NewGuid();

        //    //call required services
        //    var getCustomersResponse = _customerService.Get(new GetCustomerRequest());
        //    if (getCustomersResponse == null)
        //        throw new Exception("An error occured while getting customers");

        //    var getRestaurantsResponse = _restaurantService.Get(new GetRestaurantRequest());
        //    if (getRestaurantsResponse == null)
        //        throw new Exception("An error occured while getting restaurants");

        //    //Generate visitors
        //    customersToGo.AddRange(PickVisitors(getCustomersResponse.Customers));

        //    //Select a restaurant to go
        //    var nearRestaurant = GetNearRestaurant(getRestaurantsResponse.Restaurants);

        //    //Prepare request and execute the process
        //    CreateVisitRequest visitRequest = new CreateVisitRequest
        //    {
        //        CustomerIDList = customersToGo.Select(x => x.ID).ToList(),
        //        ReservationID = currentReservationID,
        //        RestaurantID = nearRestaurant.ID,
        //        DateIn = currentDatetime
        //    };

        //    var visitResponse = _customerService.Visit(visitRequest);

        //    return visitResponse.VisitTransactionIdList;
        //}

        #endregion Managers

        #region Helpers

        #region General Operations

        /// <summary>
        /// Builds request that contains random generated restaurant list
        /// </summary>
        /// <param name="count">how many restaurant will be created</param>
        /// <returns>CreateRestaurantRequest</returns>
        private CreateRestaurantRequest BuildCreateRestaurantRequest(int count)
        {
            //initialize variables
            var restaurants = new List<RestaurantDTO>(count);
            var tablesOfRestaurants = new List<TableDTO>();

            //Generate restaurants and its tables
            RestaurantDTO currentRestaurant;
            for (int i = 0; i < count; i++)
            {
                currentRestaurant = GenerateRestaurant();
                restaurants.Add(currentRestaurant);
                tablesOfRestaurants.AddRange(GenerateTables(currentRestaurant.ID));
            }

            return new CreateRestaurantRequest
            {
                Restaurants = restaurants,
                Tables = tablesOfRestaurants
            };
        }

        /// <summary>
        /// Returns random generated restaurantDto
        /// </summary>
        /// <returns>RestaurantDTO</returns>
        private RestaurantDTO GenerateRestaurant()
        {
            return new RestaurantDTO()
            {
                ID = Guid.NewGuid(),
                Latitude = RandomHelper.RandomDouble(Constants.LatitudeRange.Min, Constants.LatitudeRange.Max).ToGPSFormat(),
                Longitude = RandomHelper.RandomDouble(Constants.LongitudeRange.Min, Constants.LongitudeRange.Max).ToGPSFormat(),
                SupplyDayOfWeek = (byte)RandomHelper.RandomInteger(1, 7)
            };
        }

        /// <summary>
        /// Returns a restaurant selected randomly from restaurant list
        /// </summary>
        /// <param name="restaurants">all restaurants</param>
        /// <returns>instance of restaurantDTO</returns>
        private RestaurantDTO GetNearRestaurant(List<RestaurantDTO> restaurants)
        {
            return restaurants[RandomHelper.RandomInteger(0, restaurants.Count)];
        }

        #endregion General Operations

        #region Staff Operations

        /// <summary>
        /// Builds request that contains random generated staff list
        /// </summary>
        /// <param name="count">how many staff will be created</param>
        /// <returns>instance of CreateStaffRequest</returns>
        private CreateStaffRequest BuildCreateStaffRequest(int count)
        {
            List<StaffDTO> staffs = new List<StaffDTO>();
            var getRestaurantResponse = _restaurantService.Get(new GetRestaurantRequest());
            for (int i = 0; i < count; i++)
            {
                staffs.Add(GenerateStaff(getRestaurantResponse.Restaurants));
            }
            return new CreateStaffRequest() { Staffs = staffs };
        }

        /// <summary>
        /// Returns random generated staffDto
        /// </summary>
        /// <returns>instance of StaffDTO</returns>
        private StaffDTO GenerateStaff(List<RestaurantDTO> restaurants)
        {
            return new StaffDTO
            {
                ID = Guid.NewGuid(),
                WorkPlaceID = restaurants[RandomHelper.RandomInteger(0, restaurants.Count)].ID
            };
        }


        #endregion Staff Operations

        #region Table Operations

        /// <summary>
        /// Returns random generated tableDto
        /// </summary>
        /// <returns>instance of TableDTO</returns>
        private List<TableDTO> GenerateTables(Guid restaurantId)
        {
            var tableStatusCount = Enum.GetNames(typeof(TableStatus)).Length;

            var tableCountOfCurrentRestaurant = (new Random()).Next(1, Constants.MaxTableCountInRestaurant);
            List<TableDTO> tables = new List<TableDTO>(tableCountOfCurrentRestaurant);

            var addingTableCounter = default(int);
            while (tableCountOfCurrentRestaurant > addingTableCounter)
            {
                tables.Add(new TableDTO()
                {
                    ID = Guid.NewGuid(),
                    RestaurantID = restaurantId,
                    MaxCapacity = (byte)RandomHelper.RandomInteger(byte.MinValue, byte.MaxValue),
                    Status = (byte)RandomHelper.RandomInteger(0, tableStatusCount)
                });
                addingTableCounter++;
            }

            return tables;
        }
        #endregion Table Operations

        #region CustomerOperations
        //private IEnumerable<CustomerDTO> PickVisitors(List<CustomerDTO> customers)
        //{
        //    //Generate visitor count in current group and prepare a collection to collect them.
        //    var expectedVisitorCountInGroup = RandomHelper.RandomInteger(Constants.CustomerVisitorCountRangeInGroup.Min, Constants.CustomerVisitorCountRangeInGroup.Max);
        //    var visitors = new List<CustomerDTO>(expectedVisitorCountInGroup);

        //    //Generate visitors
        //    while (expectedVisitorCountInGroup > visitors.Count)
        //    {
        //        var candidateCustomer = GetVisitorCandidate(customers);
        //        if (!visitors.Any(customer => customer.ID == candidateCustomer.ID))
        //        {
        //            visitors.Add(candidateCustomer);
        //        }
        //    }

        //    return visitors;
        //}

        //private CustomerDTO GetVisitorCandidate(List<CustomerDTO> customers)
        //{
        //    CustomerDTO customerCandidate;
        //    do
        //    {
        //        customerCandidate = customers[RandomHelper.RandomInteger(0, customers.Count)];

        //    } while (customerCandidate.VisitCountInYear > _settings.CustomerSettings.MaxVisitCountInYear);

        //    return customerCandidate;
        //}
        #endregion

        #endregion Helpers
    }
}

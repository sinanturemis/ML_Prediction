using PredictionApp.Repository;
using System.Linq;
using System.Transactions;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with restaurants
    /// </summary>
    public class RestaurantService : ServiceBase
    {
        /// <summary>
        /// Repository for restaurant table
        /// </summary>
        private RestaurantRepository _restaurantRepository;

        /// <summary>
        /// Repository for 'table' table
        /// </summary>
        private TableRepository _tableRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public RestaurantService(DbSettings dbSettings) : base(dbSettings)
        {
            _restaurantRepository = new RestaurantRepository(_dbSettings.ConnectionString);
            _tableRepository = new TableRepository(_dbSettings.ConnectionString);
        }

        /// <summary>
        /// Get restaurants
        /// </summary>
        /// <param name="request">request for getting restaurants</param>
        /// <returns>returns response of the operation</returns>
        public GetRestaurantResponse Get(GetRestaurantRequest request)
        {
            //Get restaurants
            var restaurantEntities = _restaurantRepository.Get();

            //Prepare response by using restaurantEnitites
            var response = new GetRestaurantResponse();
            response.Restaurants = restaurantEntities.Select(restaurant => new RestaurantDTO
            {
                ID = restaurant.ID,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                SupplyDayOfWeek = restaurant.SupplyDayOfWeek
            }).ToList();

            return response;
        }

        /// <summary>
        /// Get restaurants by supply day of week
        /// </summary>
        /// <param name="request">request for getting restaurants by supply day of week</param>
        /// <returns>returns response of the operation</returns>
        public GetRestaurantsBySupplyDayOfWeekResponse GetBySupplyDayOfWeek(GetRestaurantsBySupplyDayOfWeekRequest request)
        {
            //Get restaurants by supply day of week
            var restaurantEntities = _restaurantRepository.GetBySupplyDayOfWeek(request.SupplyDayOfWeek);

            return new GetRestaurantsBySupplyDayOfWeekResponse()
            {
                Restaurants = restaurantEntities.Select(entity => new RestaurantDTO
                {
                    ID = entity.ID,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    SupplyDayOfWeek = entity.SupplyDayOfWeek
                }).ToList()
            };
        }

        /// <summary>
        /// Creates restaurant
        /// </summary>
        /// <param name="request">request to create restaurant</param>
        /// <returns>returns response of the operation</returns>
        public CreateRestaurantResponse CreateRestaurant(CreateRestaurantRequest request)
        {
            //Map entities from request
            var restaurantEntities = request.Restaurants.Select(restaurant => new RestaurantEntity
            {
                ID = restaurant.ID,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                SupplyDayOfWeek = restaurant.SupplyDayOfWeek
            });

            var tableEntities = request.Tables.Select(table => new TableEntity
            {
                ID = table.ID,
                RestaurantID = table.RestaurantID,
                MaxCapacity = table.MaxCapacity
            });

            //Create new db transaction
            using (TransactionScope scope = base.CreateTransactionScope())
            {
                //Add new restaurant on restaurant table
                _restaurantRepository.Add(restaurantEntities);

                //Add new tables for current restaurant
                _tableRepository.Add(tableEntities);

                //Commit transaction
                scope.Complete();
            }

            return new CreateRestaurantResponse();
        }
    }
}

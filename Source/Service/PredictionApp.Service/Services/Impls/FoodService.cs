using PredictionApp.Repository;
using System.Linq;
using System.Transactions;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with foods
    /// </summary>
    public class FoodService : ServiceBase
    {
        /// <summary>
        /// Repository for food table
        /// </summary>
        private FoodRepository _foodRepository;

        /// <summary>
        /// Repository for food recipe table
        /// </summary>
        private FoodRecipeRepository _foodRecipeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public FoodService(DbSettings dbSettings) : base(dbSettings)
        {
            _foodRepository = new FoodRepository(_dbSettings.ConnectionString);
            _foodRecipeRepository = new FoodRecipeRepository(_dbSettings.ConnectionString);
        }

        /// <summary>
        /// Creates food with its recipe
        /// </summary>
        /// <param name="request">request to create food and ingredents</param>
        public void Create(CreateFoodRequest request)
        {
            //Map request to entities
            var foodEntity = request.Foods.Select(x => new FoodEntity
            {
                ID = x.ID,
                Name = x.Name,
                Price = x.Price
            }).ToList();

            var foodRecipeEntities = request.Ingredents.Select(x => new FoodRecipeEntity
            {
                FoodID = x.FoodID,
                ProductID = x.ProductID,
                ProductAmount = x.ProductAmount
            }).ToList();

            //Create new db transaction
            using (TransactionScope scope = CreateTransactionScope())
            {
                //Add food on db
                _foodRepository.Add(foodEntity);

                //Add food recipe on db
                _foodRecipeRepository.Add(foodRecipeEntities);

                //Commit transaction
                scope.Complete();
            }
        }
    }
}

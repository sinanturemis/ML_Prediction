using PredictionApp.Repository;
using System.Linq;
using System.Transactions;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with products
    /// </summary>
    public class ProductService : ServiceBase
    {
        /// <summary>
        /// Repository for product table
        /// </summary>
        private ProductRepository _productRepository;

        /// <summary>
        /// Repository for product stock transaction table
        /// </summary>
        private ProductStockTransactionRepository _productStockTransactionRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public ProductService(DbSettings dbSettings) : base(dbSettings)
        {
            _productRepository = new ProductRepository(_dbSettings.ConnectionString);
            _productStockTransactionRepository = new ProductStockTransactionRepository(dbSettings.ConnectionString);
        }

        /// <summary>
        /// Returns filtered products by request parameter
        /// </summary>
        /// <param name="request">request for getting products</param>
        /// <returns>returns response of the operation</returns>
        public GetProductsResponse Get(GetProductsRequest request)
        {
            //Map request to entity
            var productEntities = _productRepository.Get().Select(productEntity => new ProductDTO
            {
                ID = productEntity.ID,
                SupplierID = productEntity.SupplierID,
                Name = productEntity.Name,
                UnitPrice = productEntity.UnitPrice
            });

            //Prepare response
            var response = new GetProductsResponse();

            //Map entity to response
            response.Products = productEntities.ToList();

            return response;
        }

        /// <summary>
        /// Creates product
        /// </summary>
        /// <param name="request">request to create product</param>
        /// <returns>returns response of the operation</returns>
        public CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            //Map request to entity
            var productEntities = request.Products.Select(x => new ProductEntity
            {
                ID = x.ID,
                Name = x.Name,
                SupplierID = x.SupplierID,
                UnitPrice = x.UnitPrice
            }).ToList();

            //Create new db transactions
            using (TransactionScope scope = CreateTransactionScope())
            {
                //Add product to db
                _productRepository.Add(productEntities);

                //Commit transaction
                scope.Complete();
            }

            return new CreateProductResponse();
        }

        /// <summary>
        /// Get Latest spending product transactions
        /// </summary>
        /// <param name="request">request for getting product transactions</param>
        /// <returns>returns response of the operation<</returns>
        public GetLatestStockTransactionsResponse GetLatestNegativeStockTransactions(GetLatestStockTransactionsRequest request)
        {
            //Get spending product transactions
            var productStockTransactionEntities = _productStockTransactionRepository.GetLastNegativeStockTransactions(request.RestaurantId);

            //Prepare response
            var response = new GetLatestStockTransactionsResponse();

            //Map entity to response
            response.LastStockTransactions = productStockTransactionEntities.Select(entity => new ProductStockTransactionDTO
            {
                ProductID = entity.ProductID,
                RestaurantID = entity.RestaurantID,
                TransactionAmount = entity.TransactionAmount,
                RemainingAmount = entity.RemainingAmount,
                CreatedDatetime = entity.CreatedDatetime
            }).ToList();

            return response;
        }

        /// <summary>
        /// Get latest product stock transactions filtered by restaurant and date
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetLatestStockTransactionsResponse GetLatestStockTransactions(GetLatestStockTransactionsRequest request)
        {
            //Get product stock transactions by restaurant and start date
            var productStockTransactionEntities = _productStockTransactionRepository.Get(request.RestaurantId, request.StartDate);

            //Prepare response
            var response = new GetLatestStockTransactionsResponse();

            //Map entity to response
            response.LastStockTransactions = productStockTransactionEntities.Select(entity => new ProductStockTransactionDTO
            {
                ProductID = entity.ProductID,
                RestaurantID = entity.RestaurantID,
                TransactionAmount = entity.TransactionAmount,
                RemainingAmount = entity.RemainingAmount,
                CreatedDatetime = entity.CreatedDatetime
            }).ToList();

            return response;
        }
    }

}

using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetLatestStockTransactionsResponse : ResponseBase
    {
        public List<ProductStockTransactionDTO> LastStockTransactions { get; set; }
    }
}

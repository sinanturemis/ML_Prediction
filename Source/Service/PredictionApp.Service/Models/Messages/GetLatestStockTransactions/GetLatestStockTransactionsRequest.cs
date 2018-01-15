using System;

namespace PredictionApp.Service
{
    public class GetLatestStockTransactionsRequest : RequestBase
    {
        public Guid RestaurantId { get; set; }
        public DateTime StartDate { get; set; }
    }
}

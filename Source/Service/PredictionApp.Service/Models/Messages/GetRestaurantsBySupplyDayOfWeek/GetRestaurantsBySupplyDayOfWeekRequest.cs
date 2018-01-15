namespace PredictionApp.Service
{
    public class GetRestaurantsBySupplyDayOfWeekRequest : RequestBase
    {
        public byte SupplyDayOfWeek { get; set; }
    }
}

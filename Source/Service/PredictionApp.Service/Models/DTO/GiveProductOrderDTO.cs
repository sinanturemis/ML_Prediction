using System;

namespace PredictionApp.Service
{
    public class GiveProductOrderDTO
    {
        public Guid RestaurantID { get; set; }
        public Guid StaffID { get; set; }
        public Guid ProductID { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

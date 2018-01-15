using System;

namespace PredictionApp.Repository
{
    public class StaffEntity : IEntity
    {
        public Guid ID { get; set; }
        
        //RestaurantID
        public Guid WorkplaceID { get; set; }
    }
}

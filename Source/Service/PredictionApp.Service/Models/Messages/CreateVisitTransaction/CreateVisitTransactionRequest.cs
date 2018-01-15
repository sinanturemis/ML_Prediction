using System;
using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateVisitRequest : RequestBase
    {
        public List<Guid> CustomerIDList { get; set; }
        public Guid ReservationID { get; set; }
        public Guid RestaurantID { get; set; }
        public DateTime DateIn { get; set; }
    }
}

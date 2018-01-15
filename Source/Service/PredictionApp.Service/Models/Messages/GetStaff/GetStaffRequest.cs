using System;

namespace PredictionApp.Service
{
    public class GetStaffRequest : RequestBase
    {
        public Guid? WorkplaceID { get; set; }
    }
}

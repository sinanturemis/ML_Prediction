using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateStaffRequest : RequestBase
    {
        public List<StaffDTO> Staffs { get; set; }
    }
}

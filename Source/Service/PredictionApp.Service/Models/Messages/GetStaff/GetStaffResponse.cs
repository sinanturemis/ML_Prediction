using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetStaffResponse : ResponseBase
    {
        public List<StaffDTO> Staffs { get; set; }
    }
}

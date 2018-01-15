using System;
using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateLeaveTransactionRequest : RequestBase
    {
        public Guid VisitTransactionId { get; set; }
        public DateTime DateOut { get; set; }
    }
}

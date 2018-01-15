using System;
using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateVisitResponse : ResponseBase
    {
        public List<Guid> VisitTransactionIdList { get; set; }
    }
}

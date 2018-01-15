using System.Collections.Generic;

namespace PredictionApp.Presentation.Web.Models.UIModels
{
    public class GetDailyStatusTotalNumberReportModel
    {
        public GetDailyStatusTotalNumberReportModel()
        {
            Data = new List<GetDailyStatusTotalNumberReportModelItem>();
            Data.Add(new GetDailyStatusTotalNumberReportModelItem
            {
                name = "Asd",
                data = new List<int> { 5, 8, 9 },
                color = "#FF4477"
            });
            Data.Add(new GetDailyStatusTotalNumberReportModelItem
            {
                name = "Asd4545",
                data = new List<int> { 1, 34, 78, 98 },
                color = "#FA7711"
            });
        }

        public List<GetDailyStatusTotalNumberReportModelItem> Data { get; set; }

        public class GetDailyStatusTotalNumberReportModelItem
        {
            public string name { get; set; }
            public List<int> data { get; set; }
            public string color { get; set; }
        }
    }
}
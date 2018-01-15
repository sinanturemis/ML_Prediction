using System.Web.Mvc;

namespace PredictionApp.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var model = new Models.UIModels.GetDailyStatusTotalNumberReportModel();
            return Json(model.Data, JsonRequestBehavior.AllowGet);
        }
    }
}
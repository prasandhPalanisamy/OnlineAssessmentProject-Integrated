using OnlineAssessmentApplication.Filters;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    [AuthenticationFilter]
    [LogCustomExceptionFilter]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
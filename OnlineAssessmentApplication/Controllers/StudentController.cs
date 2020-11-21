using OnlineAssessmentApplication.Filters;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    [AuthenticationFilter]
    [StudentAuthorizeFilter]
    [LogCustomExceptionFilter]

    public class StudentController : Controller
    {
        // GET: Student
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpcomingTest()
        {
            return View();
        }
    }
}
using OnlineAssessmentApplication.Filters;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    [AuthenticationFilter]
    //[StudentAuthorizeFilter]
    //[LogCustomExceptionFilter]
    public class TestController : Controller
    {
        readonly ITestService testService;
        public TestController(ITestService testService)
        {
            this.testService = testService;
        }
        public TestController()
        {
        }
        // GET: Test
        public ActionResult CreateTest()
        {

            return View();
        }
        [HttpPost]
        [ActionName("CreateTest")]
        public ActionResult SaveTest(CreateTestViewModel newTest)//Create Test
        {

            if (ModelState.IsValid)
            {
                newTest.UserId = Convert.ToInt32(Session["CurrentUserID"]);
                newTest.CreatedBy = newTest.UserId;
                newTest.CreatedTime = DateTime.Now;


                testService.CreateNewTest(newTest);
                return RedirectToAction("UpcomingTest");
            }
            return View();
        }
        public ActionResult EditTest(int testId)
        {

            TestViewModel test = testService.GetTestByTestId(testId);
            return View(test);
        }
        [HttpPost]
        public ActionResult EditTest(EditTestViewModel editedData)
        {
            if (ModelState.IsValid)
            {
                editedData.ModifiedBy = Convert.ToInt32(Session["CurrentUserID"]);
                editedData.ModifiedTime = DateTime.Now;
                testService.UpdateTest(editedData);
                return RedirectToAction("UpcomingTest");
            }
            return View();

        }
        public ActionResult DeleteTest(int testId)
        {

            testService.DeleteTest(testId);
            return RedirectToAction("UpcomingTest");
        }

        [HttpGet]
        public ActionResult UpcomingTest(FilterPanel filterPanel)
        {
            IEnumerable<TestViewModel> test = testService.DisplayAvailableTestDetails(filterPanel);
            return View(test);
        }

        public ActionResult VerifyPasscode(int passcode)
        {
            if (testService.VerifyPasscode(passcode))
                return RedirectToAction("TakeTest");
            else
            {
                TempData["Passcode_Alert"] = "Wrong Passcode.Please enter the correct passcode";
                return RedirectToAction("UpcomingTest");
            }
        }

        public ActionResult ViewScore(ResultViewModel resultViewModel)
        {
            IEnumerable<ResultViewModel> resultViewModels = testService.CalculateScore(resultViewModel);
            return View(resultViewModels);
        }
        public ActionResult UpdateAcceptStatus(int testId)
        {
            testService.UpdateAcceptStatus(testId);
            TempData["message"] = "Test Accepted Successfully";
            return RedirectToAction("index","dashboard");
        }
        public ActionResult UpdateRejectStatus(int testId)
        {
            testService.UpdateRejectStatus(testId);
            TempData["message"] = "Test Rejected Successfully";
            return RedirectToAction("index", "dashboard");
        }
    }
}
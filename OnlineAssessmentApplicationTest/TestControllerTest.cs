using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineAssessmentApplication.Controllers;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ViewModel;

namespace OnlineAssessmentApplicationTest
{
    [TestClass]
    public class TestControllerTest
    {
        readonly TestController testController;
        readonly TestRepository testRepository;
        public TestControllerTest()
        {
            testController = new TestController();
            testRepository = new TestRepository();
        }
        
        [TestMethod]
        public void UpcomingTestView()
        {
            FilterPanel filterPanel = new FilterPanel() { SearchBy = "mid", SubjectId = 5 };
            // Arrange 

            // Act
            ViewResult result = testController.UpcomingTest(filterPanel) as ViewResult;

            // Assert
            Assert.AreEqual("UpcomingTest", result);
        }


        [TestMethod]
        public void UpcomingTest()
        {
            FilterPanel filterPanel = new FilterPanel() { SearchBy = "mid", SubjectId = 5 };
            // Arrange

            // Act
            IEnumerable<Test> result = testRepository.DisplayAvailableTestDetails(filterPanel);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void VerifyPassCode()
        {
            int passcode = 2345;
            bool verify = testRepository.VerifyPasscode(passcode);
            Assert.IsTrue(verify);
        }
        //[TestMethod]
        //public void CheckAcceptStatus()
        //{
        //    int id = 3;
        //    bool status = testRepository.UpdateAcceptStatus(id);

        //}
    }
}

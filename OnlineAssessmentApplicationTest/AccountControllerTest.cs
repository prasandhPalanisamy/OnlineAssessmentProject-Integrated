using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAssessmentproject
{
    [TestClass]
    public class AccountControllerTest
    {
        readonly UserRepository userRepository;
        readonly AssessmentDbContext db;
        readonly TestRepository testRepository;
        public AccountControllerTest()
        {
            this.userRepository = new UserRepository();
            this.testRepository = new TestRepository();
            this.db = new AssessmentDbContext();
        }
        [TestMethod]
        public void CreateTest()
        {

            userRepository.Create(new User() { Name = "Manoj", EmailID = "manojradha@gmail.com", PhoneNumber = 8675674243, Password = "123", UserGrade = 2, RoleId = 1 });
            IEnumerable<User> fetchedData = db.Users.Where(temp => temp.EmailID == "manojradha@gmail.com")?.ToList();
            Assert.IsNotNull(fetchedData);

        }
        [TestMethod]
        public void EditTest()
        {
            int id = 6;
            User user = userRepository.Edit(id);
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void Updatetest()
        {
            bool value = false;
            int id = 6;
            User user = userRepository.Edit(id);
            if (user != null)
            {
                user.Name = "man";
                userRepository.Update(user);
                value = true;
            }
            Assert.IsTrue(value);

        }
        [TestMethod]
        public void DeleteTest()
        {
            bool value = false;
            int id = 7;
            User user = userRepository.Edit(id);
            if (user != null)
            {

                userRepository.Delete(id);
                value = true;
            }
            Assert.IsTrue(value);
        }


        [TestMethod]
        public void CreateNewTest() //To create new test
        {
            testRepository.CreateNewTest(new Test() { TestName = "Assessment", UserId = 6, Subject = 3, Grade = 6, TestDate = new DateTime(2020, 11, 19, 11, 50, 0), StartTime = new DateTime(2020, 11, 19, 11, 50, 0), EndTime = new DateTime(2020, 11, 19, 12, 50, 0) });
            IEnumerable<Test> fetchedData = db.Tests.Where(data => data.TestId == 6)?.ToList();
            Assert.IsNotNull(fetchedData);
        }
        [TestMethod]
        public void TestEditTest()
        {
            int id = 4;
            Test test = testRepository.GetTestByTestId(id);
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void TestDeleteTest()
        {
            bool value = false;
            int id = 4;
            Test test = testRepository.GetTestByTestId(id);
            if (test != null)
            {

                testRepository.DeleteTest(id);
                value = true;
            }
            Assert.IsTrue(value);
        }
    }
}
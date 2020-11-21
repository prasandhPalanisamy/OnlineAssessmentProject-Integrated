using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAssessmentApplication.Repository
{
    public interface ITestRepository
    {
        IEnumerable<Test> DisplayAvailableTestDetails(FilterPanel filterPanel);
        IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel);
        bool VerifyPasscode(int passcode);
        void CreateNewTest(Test test);
        Test GetTestByTestId(int testId);
        void UpdateTest(Test editTest);
        void DeleteTest(int testId);
        void UpdateAcceptStatus(int testId);
        void UpdateRejectStatus(int testId);
    }
    public class TestRepository : ITestRepository
    {
        readonly AssessmentDbContext db;
        public TestRepository()
        {
            db = new AssessmentDbContext();
        }
        public void CreateNewTest(Test test) //To create new test
        {

            db.Tests.Add(test);
            db.SaveChanges();

        }

        public void UpdateTest(Test editTest)
        {

            Test test = db.Tests.Find(editTest.TestId);
            if (test != null)
            {
                test.TestId = editTest.TestId;
                test.TestName = editTest.TestName;
                test.TestDate = editTest.TestDate;
                test.StartTime = editTest.StartTime;
                test.EndTime = editTest.EndTime;
                test.Grade = editTest.Grade;
                test.Subject = editTest.Subject;

                test.ModifiedTime = editTest.ModifiedTime;
                db.SaveChanges();
            }

        }
        public void DeleteTest(int testId)
        {
            db.Tests.Remove(GetTestByTestId(testId));
            db.SaveChanges();
        }
        public Test GetTestByTestId(int testId)
        {
            return db.Tests.Find(testId);
        }

        readonly string userName = HttpContext.Current.User.Identity.Name.ToString();

        public IEnumerable<Test> DisplayAvailableTestDetails(FilterPanel filterPanel)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                if (HttpContext.Current.User.IsInRole("Student"))
                {
                    User currentUser = AssessmentDBContext.Users.FirstOrDefault(user => user.Name == userName);
                    IEnumerable<Test> tests = AssessmentDBContext.Tests.Where(test => test.Grade==currentUser.UserGrade && test.Status.Equals("Accepted") && test.Subject.Equals(filterPanel.SubjectId)&&test.TestName.Contains(filterPanel.SearchBy) ||filterPanel.SearchBy == null|| filterPanel.SubjectId == 0).ToList();
                    return tests;
                }
                else if (HttpContext.Current.User.IsInRole("Principal"))
                {
                    IEnumerable<Test> tests = AssessmentDBContext.Tests.Where(test=> test.Subject.Equals(filterPanel.SubjectId) && test.Grade.Equals(filterPanel.GradeId) && test.TestName.Contains(filterPanel.SearchBy)|| filterPanel.SearchBy == null || filterPanel.SubjectId == 0).ToList();
                    return tests;
                }
                else if (HttpContext.Current.User.IsInRole("Teacher"))
                {
                    IEnumerable<Test> tests = AssessmentDBContext.Tests.Where(test =>test.Subject.Equals(filterPanel.SubjectId) && test.Grade.Equals(filterPanel.GradeId) && test.TestName.Contains(filterPanel.SearchBy)|| filterPanel.SearchBy == null&&filterPanel.GradeId==0||filterPanel.SubjectId==0).ToList();
                    return tests;
                }
                else
                {
                    return AssessmentDBContext.Tests.ToList();
                }
            }
        }

        public bool VerifyPasscode(int passcode)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test verifiedTest = AssessmentDBContext.Tests.Where(test => test.Passcode==passcode).FirstOrDefault();
                if (verifiedTest!=null)
                    return true;
                else
                    return false;
            }

        }

        public IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                User currentUser = AssessmentDBContext.Users.FirstOrDefault(user => user.Name == userName);
                SqlParameter userId = new SqlParameter("@UserId",currentUser.UserId);

                var resultViewModels = AssessmentDBContext.Database.SqlQuery<ResultViewModel>("SP_CalculateScore @UserId", userId).ToList();

                return resultViewModels;
            }
        }

        public void UpdateAcceptStatus(int testId)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test test = AssessmentDBContext.Tests.Find(testId);
                test.Status = "Accepted";
                AssessmentDBContext.SaveChanges();
            }
        }

        public void UpdateRejectStatus(int testId)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test test = AssessmentDBContext.Tests.Find(testId);
                test.Status = "Rejected";
                AssessmentDBContext.SaveChanges();
            }
        }

    }
}

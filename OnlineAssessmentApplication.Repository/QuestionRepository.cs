using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAssessmentApplication.Repository
{
    public interface IQuestionRepository
    {
        int InsertQuestion(Questions question);
        void EditQuestion(Questions editQuestion);
        void DeleteQuestion(int testId);
        Questions GetQuestionsByTestId(int testId);
        IEnumerable<Questions> DisplayAllQuestions(int testId);

    }
    public class QuestionRepository : IQuestionRepository
    {
        AssessmentDbContext db;
        public QuestionRepository()
        {
            db = new AssessmentDbContext();
        }
        public int InsertQuestion(Questions question)
        {
            
            db.Questions.Add(question);
            db.SaveChanges();
            return question.QuestionId;
        }

        public void EditQuestion(Questions editQuestion)
        {
            Questions question = db.Questions.Find(editQuestion.QuestionId);
            if (question != null)
            {
                question.TestId = editQuestion.TestId;
                question.QuestionId = editQuestion.QuestionId;
                question.Question = editQuestion.Question;
                question.CreatedDate = editQuestion.CreatedDate;
                question.ModifiedDate = editQuestion.ModifiedDate;
                db.SaveChanges();
            }
        }
        public void DeleteQuestion(int questionId)
        {
            db.Questions.Remove(GetQuestionsByTestId(questionId));
            db.SaveChanges();
        }
        public Questions GetQuestionsByTestId(int questionId)
        {
            return db.Questions.Find(questionId);
        }

        public IEnumerable<Questions> DisplayAllQuestions(int testId)
        {

            IEnumerable<Questions> allQuestions = db.Questions.Where(temp => temp.TestId == testId).ToList();
            return allQuestions;

        }
    }
}

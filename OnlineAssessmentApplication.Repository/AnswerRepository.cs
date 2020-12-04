using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAssessmentApplication.Repository
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answer answer);
        void EditAnswer(Answer editAnswer);
        void DeleteAnswer(int answerId);
        Answer GetAnswersByAnswerId(int answerId);
        IEnumerable<Answer> DisplayAnswers(int questionId);
    }
    public class AnswerRepository : IAnswerRepository
    {
        readonly private AssessmentDbContext db;
        public AnswerRepository()
        {
            db = new AssessmentDbContext();
        }
        public void InsertAnswer(Answer answer)
        {
            db.Answers.Add(answer);
            db.SaveChanges();
        }
        public void EditAnswer(Answer editAnswer)
        {
            Answer answer = db.Answers.Find(editAnswer.AnswerId);
            if (answer != null)
            {
                answer.AnswerId = editAnswer.AnswerId;
                answer.AnswerLabel = editAnswer.AnswerLabel;
                answer.AnswerDescription = editAnswer.AnswerDescription;
                answer.Mark = editAnswer.Mark;
                answer.IsCorrect = editAnswer.IsCorrect;
                db.SaveChanges();
            }
        }
        public void DeleteAnswer(int answerId)
        {
            db.Answers.Remove(GetAnswersByAnswerId(answerId));
            db.SaveChanges();
        }
        public Answer GetAnswersByAnswerId(int answerId)
        {
            return db.Answers.Find(answerId);
        }
        public IEnumerable<Answer> DisplayAnswers(int questionId)
        {
            IEnumerable<Answer> allAnswers = db.Answers.Where(answer => answer.QuestionId == questionId).ToList();
            return allAnswers;
        }
    }
}

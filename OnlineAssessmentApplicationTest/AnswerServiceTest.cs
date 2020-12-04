using System.Collections.Generic;
using System.Web.Mvc;
using OnlineAssessmentApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;

namespace OnlineAssessmentApplicationTest
{
    [TestClass]
    public class AnswerServiceTest
    {
        private readonly AnswerService answerService;
        private readonly Mock<IAnswerRepository> answerMock = new Mock<IAnswerRepository>();
        public AnswerServiceTest()
        {
            answerService = new AnswerService(answerMock.Object);
        }
        [TestMethod]
        public void GetAnswersByAnswerId()
        {
            var answerId = 19;
            answerMock.Setup(a => a.GetAnswersByAnswerId(answerId)).Returns(new Answer() { AnswerId = answerId});
            var test = answerService.GetAnswersByAnswerId(answerId);
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void InsertAnswer()
        {
            answerMock.Setup(a => a.InsertAnswer(It.IsAny<Answer>()));            
            answerService.InsertAnswer(new AnswerViewModel() { AnswerId = 5, AnswerDescription = "Yes", AnswerLabel = "a", Mark = 1, IsCorrect = true, QuestionId = 5 });
            answerMock.Verify(a => a.InsertAnswer(It.IsAny<Answer>()), Times.Once);
        }
        [TestMethod]
        public void EditAnswer()
        {
            answerMock.Setup(a => a.EditAnswer(It.IsAny<Answer>()));
            answerService.EditAnswer(new AnswerViewModel() { AnswerId = 5, AnswerDescription = "Yes", AnswerLabel = "a", Mark = 1, IsCorrect = true, QuestionId = 5 });
            answerMock.Verify(a => a.EditAnswer(It.IsAny<Answer>()), Times.Once);
        }
        [TestMethod]
        public void DeleteAnswer()
        {
            var answerId = 5;
            answerMock.Setup(a => a.DeleteAnswer(answerId));

            answerService.DeleteAnswer(answerId);
            answerMock.Verify(a => a.DeleteAnswer(answerId), Times.Once);
        }
    }
}

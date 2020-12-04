using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineAssessmentApplication.Controllers;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;

namespace OnlineAssessmentApplicationTest
{
    [TestClass]
    public class QuestionServiceTest
    {
        private readonly QuestionService questionService;
        private readonly Mock<IQuestionRepository> questionMock = new Mock<IQuestionRepository>();
        public QuestionServiceTest()
        {
            questionService = new QuestionService(questionMock.Object);
        }
        [TestMethod]
        public void GetQuestionsByQuestionId()
        {
            var questionId = 5;

            questionMock.Setup(q => q.GetQuestionsByQuestionId(questionId)).Returns(new Questions() { QuestionId = questionId });
            var test = questionService.GetQuestionsByQuestionId(questionId);

            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void InsertQuestion()
        {
            questionMock.Setup(q => q.InsertQuestion(It.IsAny<Questions>()));


            questionService.InsertQuestion(new QuestionViewModel() { Question = "Is 2 the only even prime number ?", QuestionId = 19, TestId = 8 });
            questionMock.Verify(q => q.InsertQuestion(It.IsAny<Questions>()), Times.Once);

        }
        [TestMethod]
        public void EditQuestion()
        {
            questionMock.Setup(q => q.EditQuestion(It.IsAny<Questions>()));
            questionService.EditQuestion(new QuestionViewModel() { Question = "Is 2 the only even prime number ?", QuestionId = 19, TestId = 8 });
            questionMock.Verify(q => q.EditQuestion(It.IsAny<Questions>()), Times.Once);

        }
        [TestMethod]
        public void DeleteQuestion()
        {
            var questionId = 5;
            questionMock.Setup(q => q.DeleteQuestion(questionId));

            questionService.DeleteQuestion(questionId);
            questionMock.Verify(q => q.DeleteQuestion(questionId), Times.Once);
        }
    }
}

using OnlineAssessmentApplication.Filters;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    public class AnswerController : Controller
    {
        readonly IAnswerService answerService;
        public AnswerController()
        {

        }
        public AnswerController(IAnswerService service)
        {
            this.answerService = service;
        }
        
        public ActionResult CreateAnswer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAnswer(IList<AnswerViewModel> answerView)
        {
             if (ModelState.IsValid)
            {                
                for(int i = 0; i < answerView.Count; i++)
                {
                    answerView[i].QuestionId = (int)TempData.Peek("QuestionId");
                    answerService.InsertAnswer(answerView[i]);
                }
                
                return RedirectToAction("DisplayQuestions", "Question", new { testId = TempData.Peek("TestId")});
            }
            return View();
        }
        public ActionResult EditAnswer(int answerId)
        {
            AnswerViewModel answer = answerService.GetAnswersByAnswerId(answerId);
            return View(answer);
        }
        [HttpPost]
        public ActionResult EditAnswer(AnswerViewModel editedData)
        {
            int TestId = (int)TempData.Peek("TestId");
            if (ModelState.IsValid)
            {
                answerService.EditAnswer(editedData);
                return RedirectToAction("DisplayAnswers", new { questionId = editedData.QuestionId, testId = TestId });
            }
            return View();

        }
        public ActionResult DeleteAnswer(int answerId)
        {
            int TestId = (int)TempData.Peek("TestId");
            int QuestionId = (int)TempData.Peek("QuestionId");
            answerService.DeleteAnswer(answerId);
            return RedirectToAction("DisplayAnswers", new { questionId = QuestionId, testId = TestId });
        }
        
        public ActionResult DisplayAnswers(int questionId, int testId)
        {
            ViewData["TestId"] = (int)testId;
            IEnumerable<AnswerViewModel> answers = answerService.DisplayAnswers(questionId);
            return View(answers);
        }
    }
}
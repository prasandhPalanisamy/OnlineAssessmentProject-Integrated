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
        AnswerService answerService;
        public AnswerController()
        {
            answerService = new AnswerService();
        }
        // GET: Answer
        public ActionResult CreateAnswer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAnswer(AnswerViewModel answerView, string command)
        {
            answerView.QuestionId = (int)TempData.Peek("QuestionId");
            if (ModelState.IsValid)
            {
                answerService.InsertAnswer(answerView);
                if (command == "Add")
                {
                    return RedirectToAction("CreateAnswer", "Answer");
                }
                else if (command == "Submit")
                {
                    return RedirectToAction("CreateQuestions", "Question");
                }
            }

            return View();
        }
        public ActionResult EditAnswer(int answerId)
        {
            AnswerViewModel answer = answerService.GetAnswersByQuestionID(answerId);
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
using OnlineAssessmentApplication.Filters;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    public class QuestionController : Controller
    {
        QuestionService questionService;
        public QuestionController()
        {
            questionService = new QuestionService();
        }
        // GET: Question
        public ActionResult CreateQuestions()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CreateQuestions")]
        public ActionResult SaveQuestions(QuestionViewModel newQuestion, string Command)
        {
            newQuestion.TestId = (int)TempData.Peek("TestId");
            TempData["TestId"] = newQuestion.TestId;
            int questionId = 0;
            if (ModelState.IsValid)
            {
                questionId = questionService.InsertQuestion(newQuestion);
                TempData["QuestionId"] = questionId;
                if (Command == "Create Options")
                {
                    return RedirectToAction("CreateAnswer", "Answer");
                }

            }
            return View();
        }
        public ActionResult DisplayQuestions(int testId)
        {

            IEnumerable<QuestionViewModel> questions = questionService.DisplayAllDetails(testId);
            return View(questions);
        }

        public ActionResult EditQuestion(int questionId)
        {
            QuestionViewModel questions = questionService.GetQuestionsByTestId(questionId);
            return View(questions);
        }
        [HttpPost]
        public ActionResult EditQuestion(QuestionViewModel editedData)
        {
            //editedData.TestId = (int)TempData.Peek("MyData");
            if (ModelState.IsValid)
            {
                questionService.EditQuestion(editedData);
                return RedirectToAction("DisplayQuestions", new { testId = editedData.TestId });
            }
            return View();
        }
        public ActionResult DeleteQuestion(int questionId)
        {
            int TestId = (int)TempData.Peek("TestId");
            questionService.DeleteQuestion(questionId);
            return RedirectToAction("DisplayQuestions", new { testId = TestId });
        }
    }
}

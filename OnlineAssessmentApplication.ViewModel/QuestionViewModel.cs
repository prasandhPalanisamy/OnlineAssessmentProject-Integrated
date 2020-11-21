using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAssessmentApplication.ViewModel
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Question name is required")]
        public string Question { get; set; }
        public int TestId { get; set; }
    }
}

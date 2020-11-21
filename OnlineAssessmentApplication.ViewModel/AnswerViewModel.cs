using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAssessmentApplication.ViewModel
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Answer Label is Required")]
        public string AnswerLabel { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string AnswerDescription { get; set; }
        [Required(ErrorMessage = "Mark is Required")]
        public int Mark { get; set; }
        [Required(ErrorMessage = "IsCorrect is Required")]
        public bool IsCorrect { get; set; }
    }
}

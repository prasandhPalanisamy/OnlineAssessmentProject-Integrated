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
        [Display(Name = "Answer Label")]
        [Required(ErrorMessage = "Answer Label is Required")]        
        public string AnswerLabel { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [Display(Name = "Answer Description")]
        public string AnswerDescription { get; set; }
        [Required(ErrorMessage = "Mark is Required")]
        [Display(Name = "Mark")]
        public int Mark { get; set; }
        [Required(ErrorMessage = "IsCorrect is Required")]
        [Display(Name = "IsCorrect")]
        public bool IsCorrect { get; set; }
    }
}

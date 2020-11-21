using System.ComponentModel.DataAnnotations;

namespace OnlineAssessmentApplication.ViewModel
{
    public class EditUserPasswordViewModel
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "New Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
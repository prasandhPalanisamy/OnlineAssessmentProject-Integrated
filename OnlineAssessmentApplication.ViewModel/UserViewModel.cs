using System.ComponentModel.DataAnnotations;

namespace OnlineAssessmentApplication.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Email ID")]
        [EmailAddress(ErrorMessage = "Please enter valid Email Id")]
        public string EmailID { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Please enter valid password")]
        public string Password { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public long PhoneNumber { get; set; }
    }
}
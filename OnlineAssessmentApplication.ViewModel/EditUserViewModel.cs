using System.ComponentModel.DataAnnotations;

namespace OnlineAssessmentApplication.ViewModel
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Mobile number")]
        public long PhoneNumber { get; set; }
        public int UserId { get; set; }
    }
}

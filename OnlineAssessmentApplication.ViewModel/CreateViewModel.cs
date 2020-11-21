using OnlineAssessmentApplication.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAssessmentApplication.ViewModel
{
    public class CreateViewModel
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name required")]

        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid Email Id")]
        [Display(Name = "Email ID")]

        public string EmailID { get; set; }
        [Required]

        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[6789]\d{9}$")]
        [Display(Name = "Phone number")]
        public long PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Grade")]
        [Range(1, 11)]
        public UserGrade UserGrade { get; set; }

        [Required]
        //[Display(Name = "Role")]
        [Range(1, 4)]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }


    }
    public enum UserGrade
    {
        I = 1, II, III, IV, V, VI, VII, VIII, IX, X, others
    }
}

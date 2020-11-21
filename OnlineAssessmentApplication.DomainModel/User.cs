using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAssessmentApplication.DomainModel
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string EmailID { get; set; }

        public string Password { get; set; }


        public int UserGrade { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        //date time
        public int RoleId { get; set; }
        [ForeignKey("RoleId ")]


        public virtual Role Role { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

    }
    

}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAssessmentApplication.DomainModel
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }

        public string TestName { get; set; }
        public string Status { get; set; }
        public int Subject { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public DateTime TestDate { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int Grade { get; set; }
        public int Passcode { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

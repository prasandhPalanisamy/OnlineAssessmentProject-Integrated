using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAssessmentApplication.DomainModel
{
    public class StudentResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentResponseId { get; set; }
        public int UserId { get; set; }

        public string AnswerLabel { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]

        public Questions Questions { get; set; }
    }
}

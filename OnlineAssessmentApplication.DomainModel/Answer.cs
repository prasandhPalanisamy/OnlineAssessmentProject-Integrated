using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAssessmentApplication.DomainModel
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }

        public string AnswerLabel { get; set; }

        public string AnswerDescription { get; set; }

        public byte Mark { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Questions Questions { get; set; }
    }
}

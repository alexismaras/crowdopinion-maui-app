using CommunityToolkit.Mvvm.ComponentModel;
using Postgrest.Attributes;
using Postgrest.Models;

namespace CrowdOpinion.Models
{
    [Table("question_objects")]
    public class QuestionObjectSupa : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("question")]
        public string Question { get; set; }

        [Column("answer_one")]
        public string AnswerOne { get; set; }

        [Column("answer_two")]
        public string AnswerTwo { get; set; }

        [Column("answer_one_count")]
        public int AnswerOneCount { get; set; }

        [Column("answer_two_count")]
        public int AnswerTwoCount { get; set; }

        [Column("created_at", ignoreOnInsert: true)]
        public DateTime CreatedAt { get; set; }

    }
}

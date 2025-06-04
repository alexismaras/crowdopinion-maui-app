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

        [Column("aswer_one")]
        public string AnswerOne { get; set; }

        [Column("aswer_two")]
        public string AnswerTwo { get; set; }

        [Column("aswer_one_count")]
        public int AnswerOneCount { get; set; }

        [Column("aswer_two_count")]
        public int AnswerTwoCount { get; set; }

        [Column("created_at", ignoreOnInsert: true)]
        public DateTime CreatedAt { get; set; }

    }
}

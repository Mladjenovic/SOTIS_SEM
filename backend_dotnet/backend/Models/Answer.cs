using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Answer
    {
        public Answer()
        {
            TestResultsAnswers = new HashSet<TestResultsAnswer>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        public bool? Correct { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("Answers")]
        public virtual Question Question { get; set; }
        [InverseProperty(nameof(TestResultsAnswer.Answer))]
        public virtual ICollection<TestResultsAnswer> TestResultsAnswers { get; set; }
    }
}

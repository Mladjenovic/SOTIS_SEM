using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class TestResultsAnswer
    {
        [Key]
        public int TestResultId { get; set; }
        [Key]
        public int AnswerId { get; set; }

        [ForeignKey(nameof(AnswerId))]
        [InverseProperty("TestResultsAnswers")]
        public virtual Answer Answer { get; set; }
        [ForeignKey(nameof(TestResultId))]
        [InverseProperty("TestResultsAnswers")]
        public virtual TestResult TestResult { get; set; }
    }
}

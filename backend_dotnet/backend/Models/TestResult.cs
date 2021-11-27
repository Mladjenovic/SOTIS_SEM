using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class TestResult
    {
        public TestResult()
        {
            TestResultsAnswers = new HashSet<TestResultsAnswer>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Date { get; set; }
        public double? Points { get; set; }
        public int? Grade { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("TestResults")]
        public virtual Student Student { get; set; }
        [ForeignKey(nameof(TestId))]
        [InverseProperty("TestResults")]
        public virtual Test Test { get; set; }
        [InverseProperty(nameof(TestResultsAnswer.TestResult))]
        public virtual ICollection<TestResultsAnswer> TestResultsAnswers { get; set; }
    }
}

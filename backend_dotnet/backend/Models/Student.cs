using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Student
    {
        public Student()
        {
            TestResults = new HashSet<TestResult>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [InverseProperty(nameof(TestResult.Student))]
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}

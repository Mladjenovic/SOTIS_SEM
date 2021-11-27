using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Test
    {
        public Test()
        {
            Sections = new HashSet<Section>();
            TestResults = new HashSet<TestResult>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(120)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public double? MinimumPoints { get; set; }
        public int ProfesorId { get; set; }
        public int SubjectId { get; set; }

        [ForeignKey(nameof(ProfesorId))]
        [InverseProperty("Tests")]
        public virtual Profesor Profesor { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("Tests")]
        public virtual Subject Subject { get; set; }
        [InverseProperty(nameof(Section.Test))]
        public virtual ICollection<Section> Sections { get; set; }
        [InverseProperty(nameof(TestResult.Test))]
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}

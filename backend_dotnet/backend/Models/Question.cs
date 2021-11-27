using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        public double? PointsPerQuestion { get; set; }
        public int SectionId { get; set; }
        public int? ProblemId { get; set; }

        [ForeignKey(nameof(ProblemId))]
        [InverseProperty("Questions")]
        public virtual Problem Problem { get; set; }
        [ForeignKey(nameof(SectionId))]
        [InverseProperty("Questions")]
        public virtual Section Section { get; set; }
        [InverseProperty(nameof(Answer.Question))]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}

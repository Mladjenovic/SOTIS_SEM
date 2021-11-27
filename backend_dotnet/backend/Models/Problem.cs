using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Problem
    {
        public Problem()
        {
            Questions = new HashSet<Question>();
            Sumarises = new HashSet<Sumarise>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column("SubjectID")]
        public int? SubjectId { get; set; }
        public int? SumarizeId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("Problems")]
        public virtual Subject Subject { get; set; }
        [ForeignKey(nameof(SumarizeId))]
        [InverseProperty(nameof(Sumarise.Problems))]
        public virtual Sumarise Sumarize { get; set; }
        [InverseProperty(nameof(Question.Problem))]
        public virtual ICollection<Question> Questions { get; set; }
        [InverseProperty(nameof(Sumarise.Problem))]
        public virtual ICollection<Sumarise> Sumarises { get; set; }
    }
}

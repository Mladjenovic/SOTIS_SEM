using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Subject
    {
        public Subject()
        {
            KnowledgeSpaces = new HashSet<KnowledgeSpace>();
            Problems = new HashSet<Problem>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int Id { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public double? MinimumPoints { get; set; }
        public int? ProfesorId { get; set; }

        [ForeignKey(nameof(ProfesorId))]
        [InverseProperty("Subjects")]
        public virtual Profesor Profesor { get; set; }
        [InverseProperty(nameof(KnowledgeSpace.Subject))]
        public virtual ICollection<KnowledgeSpace> KnowledgeSpaces { get; set; }
        [InverseProperty(nameof(Problem.Subject))]
        public virtual ICollection<Problem> Problems { get; set; }
        [InverseProperty(nameof(Test.Subject))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}

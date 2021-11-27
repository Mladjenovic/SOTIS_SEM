using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Sumarise
    {
        public Sumarise()
        {
            Problems = new HashSet<Problem>();
        }

        [Key]
        public int Id { get; set; }
        public int? ProblemId { get; set; }
        public int? KnowledgeSpaceId { get; set; }

        [ForeignKey(nameof(KnowledgeSpaceId))]
        [InverseProperty("Sumarises")]
        public virtual KnowledgeSpace KnowledgeSpace { get; set; }
        [ForeignKey(nameof(ProblemId))]
        [InverseProperty("Sumarises")]
        public virtual Problem Problem { get; set; }
        [InverseProperty("Sumarize")]
        public virtual ICollection<Problem> Problems { get; set; }
    }
}

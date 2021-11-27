using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class KnowledgeSpace
    {
        public KnowledgeSpace()
        {
            Sumarises = new HashSet<Sumarise>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public int? SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("KnowledgeSpaces")]
        public virtual Subject Subject { get; set; }
        [InverseProperty(nameof(Sumarise.KnowledgeSpace))]
        public virtual ICollection<Sumarise> Sumarises { get; set; }
    }
}

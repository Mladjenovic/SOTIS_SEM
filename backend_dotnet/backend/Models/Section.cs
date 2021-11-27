using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Section
    {
        public Section()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        [InverseProperty("Sections")]
        public virtual Test Test { get; set; }
        [InverseProperty(nameof(Question.Section))]
        public virtual ICollection<Question> Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    [Keyless]
    public partial class ProblemsStudent
    {
        public int? StudentId { get; set; }
        public int? ProblemId { get; set; }

        [ForeignKey(nameof(ProblemId))]
        public virtual Problem Problem { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
    }
}

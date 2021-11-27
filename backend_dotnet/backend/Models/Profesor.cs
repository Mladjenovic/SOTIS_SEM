using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace backend.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Subjects = new HashSet<Subject>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [InverseProperty(nameof(Subject.Profesor))]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty(nameof(Test.Profesor))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}

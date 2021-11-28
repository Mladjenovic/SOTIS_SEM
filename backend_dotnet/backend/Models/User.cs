using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string UserType { get; set; }
    }
}

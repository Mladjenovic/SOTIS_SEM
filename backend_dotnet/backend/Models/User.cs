using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ApplicationIdentityUser :  IdentityUser<int>
    {
        public string FullName { get; set; }

        public string UserType { get; set; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}

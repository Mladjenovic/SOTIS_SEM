using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class ProfesorWithUser : Profesor
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
    }
}

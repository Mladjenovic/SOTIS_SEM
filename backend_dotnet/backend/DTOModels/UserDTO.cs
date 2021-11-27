using backend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOModels
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string UserType { get; set; }

    }
}

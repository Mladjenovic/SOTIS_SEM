using backend.Data;
using backend.DTOModels;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersContoller : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private DbSotisContext _context;

        public UsersContoller(SignInManager<User> signInManager, UserManager<User> userManager, DbSotisContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostUser(UserDTO model)
        {
            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.Fullname,
                UserType = model.UserType.ToString()
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (user.UserType == "Admin")
                {
                    _context.Admins.Add(new Admin()
                    {
                        UserId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            } 
        }
    }
}

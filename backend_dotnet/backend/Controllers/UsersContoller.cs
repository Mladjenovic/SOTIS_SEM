using backend.Data;
using backend.DTOModels;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private UserManager<ApplicationIdentityUser> _userManager;
        private SignInManager<ApplicationIdentityUser> _signInManager;
        private DbSotisContext _context;
        private AuthenticationContext _authContext;

        public UsersContoller(SignInManager<ApplicationIdentityUser> signInManager, UserManager<ApplicationIdentityUser> userManager, DbSotisContext context, AuthenticationContext authContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _authContext = authContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostUser(UserDTO model)
        {
            var user = new ApplicationIdentityUser()
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
                if (user.UserType == "Student")
                {
                    _context.Students.Add(new Student()
                    {
                        UserId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }
                if (user.UserType == "Profesor")
                {
                    _context.Profesors.Add(new Profesor()
                    {
                        UserId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }

                RegistredUserReturn retVal = new RegistredUserReturn()
                {
                    UserName = user.UserName,
                    UserType = user.UserType
                };
                return Ok(JsonConvert.SerializeObject(retVal));
            }
            catch (Exception)
            {
                throw;
            } 
        }

        [HttpPost]
        [Route("Login")]
        public async Task<Object> LoginUser(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // user is valid do whatever you want
                LoginUserReturn retVal = new LoginUserReturn()
                {
                    UserName = user.UserName,
                    UserType = user.UserType
                };
                return Ok(JsonConvert.SerializeObject(retVal));
            }

            return StatusCode(404, "Wrong username or passwrod");
        }
    }
}

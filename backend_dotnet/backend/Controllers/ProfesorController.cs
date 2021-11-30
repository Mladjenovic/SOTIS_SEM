using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorRepository _repository;

        private readonly DbSotisContext _context;
        private UserManager<ApplicationIdentityUser> _userManager;


        public ProfesorController(IProfesorRepository repository, DbSotisContext context, UserManager<ApplicationIdentityUser> userManager)
        {
            _repository = repository;
            _context = context;
            _userManager = userManager;
        }

        //api/profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> getProfesors()
        {
            return await _repository.GetProfesors();
        }

        [Route("ProfesorsVerbose")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> getProfesorsVerbose()
        {
            var profesors = await _context.Profesors.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            List<ProfesorWithUser> retVal = new List<ProfesorWithUser>();
            foreach (var prof in profesors)
            {
                foreach (var user in users)
                {
                    if(prof.UserId == user.Id)
                    {
                        retVal.Add(new ProfesorWithUser() 
                        { 
                            Id = prof.Id,
                            UserId = prof.UserId,
                            Fullname = user.FullName,
                            Username = user.UserName 
                        }
                                  );
                    }
                }
            }

            return retVal;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            return await _repository.GetProfesor(id);
        }

        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            return await _repository.PostProfesor(profesor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Profesor>> PutProfesor(int id, Profesor profesor)
        {
            return await _repository.PutProfesor(id, profesor);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTest(int id)
        {
            await _repository.DeleteProfesor(id);
        }

    }
}

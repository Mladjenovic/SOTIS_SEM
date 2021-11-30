using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly DbSotisContext _context;

        public ProfesorRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteProfesor(int id)
        {
            var profesor = await _context.Profesors.FindAsync(id);
            if (profesor != null)
            {
                _context.Remove(profesor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            return await _context.Profesors.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesors()
        {
            var profesors = await _context.Profesors.ToListAsync();
            var subjects = await _context.Subjects.ToListAsync();
            foreach (var prof in profesors)
            {
                foreach (var sub in subjects)
                {
                    if(sub.ProfesorId == prof.Id)
                    {
                        prof.Subjects.Add(sub);
                    }
                }
            }
            return profesors;
        }

        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            _context.Profesors.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<ActionResult<Profesor>> PutProfesor(int id, Profesor profesor)
        {
            var find = await _context.Profesors.FindAsync(id);
            if (find != null)
            {
                find.UserId = profesor.UserId;
            }

            return find;
        }
    }
}

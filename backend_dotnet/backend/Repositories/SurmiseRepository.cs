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
    public class SurmiseRepository : ISurmiseRepository
    {
        private readonly DbSotisContext _context;

        public SurmiseRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteSurmise(int id)
        {

            var surmise = await _context.Sumarises.FindAsync(id);
            if (surmise != null)
            {
                _context.Remove(surmise);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Sumarise>> GetSurmise(int id)
        {
            return await _context.Sumarises.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Sumarise>>> GetSurmises()
        {
            var surmises = await _context.Sumarises.ToListAsync();
            foreach (var surmise in surmises)
            {
                _context.Entry(surmise).Reference(item => item.Problem).Load();
                _context.Entry(surmise).Reference(item => item.KnowledgeSpace).Load();
            }
            return surmises;
        }

        public async Task<ActionResult<Sumarise>> PostSurmise(Sumarise sumarise)
        {
            _context.Sumarises.Add(sumarise);
            await _context.SaveChangesAsync();
            return sumarise;
        }

        public async Task<ActionResult<Sumarise>> PutSurmise(int id, Sumarise sumarise)
        {
            var find = await _context.Sumarises.FindAsync(id);
            if (find != null)
            {
                find.ProblemId = sumarise.ProblemId;
                find.KnowledgeSpaceId = sumarise.KnowledgeSpaceId;
            }

            return find;
        }
    }
}

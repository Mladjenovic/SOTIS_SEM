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
    public class SectionRepository : ISectionRepository
    {
        private readonly DbSotisContext _context;

        public SectionRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Remove(section);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Section>> GetSection(int id)
        {
            return await _context.Sections.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            var sections = await _context.Sections.ToListAsync();
            foreach (var section in sections)
            {
                _context.Entry(section).Reference(item => item.Test).Load();
            }
            return sections;
        }

        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return section;
        }

        public async Task<ActionResult<Section>> PutSection(int id, Section section)
        {
            var find = await _context.Sections.FindAsync(id);
            if (find != null)
            {
                find.Name = section.Name;
                find.TestId = section.TestId;
            }

            return find;
        }
    }
}

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
    public class KnowledgeSpaceRepository : IKnowledgeSpaceRepository
    {
        private readonly DbSotisContext _context;

        public KnowledgeSpaceRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteKnowledgeSpace(int id)
        {
            var knowledgeSpace = await _context.KnowledgeSpaces.FindAsync(id);
            if (knowledgeSpace != null)
            {
                _context.Remove(knowledgeSpace);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<KnowledgeSpace>> GetKnowledgeSpace(int id)
        {
            return await _context.KnowledgeSpaces.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<KnowledgeSpace>>> GetKnowledgeSpaces()
        {
            var knowledgeSpaces = await _context.KnowledgeSpaces.ToListAsync();
            foreach (var ks in knowledgeSpaces)
            {
                _context.Entry(ks).Reference(item => item.Subject).Load();
            }
            return knowledgeSpaces;
        }

        public async Task<ActionResult<KnowledgeSpace>> PostKnowledgeSpace(KnowledgeSpace knowledgeSpace)
        {
            _context.KnowledgeSpaces.Add(knowledgeSpace);
            await _context.SaveChangesAsync();
            return knowledgeSpace;
        }

        public async Task<ActionResult<KnowledgeSpace>> PutKnowledgeSpace(int id, KnowledgeSpace knowledgeSpace)
        {
            var find = await _context.KnowledgeSpaces.FindAsync(id);
            if (find != null)
            {
                find.Name = knowledgeSpace.Name;
                find.SubjectId = knowledgeSpace.SubjectId;
            }

            return find;
        }

    }
}

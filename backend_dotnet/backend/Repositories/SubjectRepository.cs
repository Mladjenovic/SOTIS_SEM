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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DbSotisContext _context;

        public SubjectRepository(DbSotisContext context)
        {
            _context = context;
        }
        public async Task DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {

            var subjects = await _context.Subjects.ToListAsync();
            foreach (var subject in subjects)
            {
                _context.Entry(subject).Reference(item => item.Profesor).Load();
            }
            return subjects;
        }

        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;

        }

        public async Task<ActionResult<Subject>> PutSubject(int id, Subject subject)
        {
            var find = await _context.Subjects.FindAsync(id);
            if (find != null)
            {
                find.Name = subject.Name;
                find.ProfesorId = subject.ProfesorId;
                find.Title = subject.Title;
            }

            return find;
        }
    }
}

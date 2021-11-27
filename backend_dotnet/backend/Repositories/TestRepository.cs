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
    public class TestRepository : ITestRepository
    {
        private readonly DbSotisContext _context;
        public TestRepository(DbSotisContext context)
        {
            _context = context;
        }
        public async Task DeleteTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if(test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            _context.Entry(test).Reference(item => item.Profesor).Load();
            _context.Entry(test).Reference(item => item.Subject).Load();
            return test;
        }

        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            var tests =  await _context.Tests.ToListAsync();
            foreach (var test in tests)
            {
                _context.Entry(test).Reference(item => item.Profesor).Load();
                _context.Entry(test).Reference(item => item.Subject).Load();
            }
            return tests;
        }

        public async Task<ActionResult<Test>> PostTest(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<ActionResult<Test>> PutTest(int id, Test test)
        {
            var find = await _context.Tests.FindAsync(id);
            if(find != null)
            {
                find.Description = test.Description;
                find.MinimumPoints = test.MinimumPoints;
                find.ProfesorId = test.ProfesorId;
                find.SubjectId = test.SubjectId;
                find.Title = test.Title;
                await _context.SaveChangesAsync();
            }
            return find;
        }
    }
}

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
    public class ProblemRepository : IProblemRespository
    {
        private readonly DbSotisContext _context;

        public ProblemRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteProblem(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            if (problem != null)
            {
                _context.Remove(problem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Problem>> GetProblem(int id)
        {
            return await _context.Problems.FindAsync(id);

        }

        public async Task<ActionResult<IEnumerable<Problem>>> GetProblems()
        {
            var problems = await _context.Problems.ToListAsync();
            foreach (var problem in problems)
            {
                _context.Entry(problem).Reference(item => item.Sumarize).Load();
                _context.Entry(problem).Reference(item => item.Subject).Load();
            }
            return problems;
        }

        public async Task<ActionResult<Problem>> PostProblem(Problem problem)
        {
            _context.Problems.Add(problem);
            await _context.SaveChangesAsync();
            return problem;
        }

        public async Task<ActionResult<Problem>> PutProblem(int id, Problem problem)
        {
            var find = await _context.Problems.FindAsync(id);
            if (find != null)
            {
                find.Name = problem.Name;
                find.SubjectId = problem.SubjectId;
                find.SumarizeId = problem.SumarizeId;
            }

            return find;
        }
    }
}

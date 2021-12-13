using backend.Data;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
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
    public class ProblemController : ControllerBase
    {
        private readonly IProblemRespository _repository;
        private readonly DbSotisContext _context;

        public ProblemController(DbSotisContext context, IProblemRespository repository)
        {
            _context = context;
            _repository = repository;
        }

        [Route("ProblemRelatedToSubject/{subjectId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblemsRelatedToSubject(int subjectId)
        {
            var problems = await _context.Problems.ToListAsync();
            var subject =  await _context.Subjects.FindAsync(subjectId);

            List<Problem> retVal = new List<Problem>();
            foreach (var problem in problems)
            {
               if(problem.SubjectId == subject.Id)
                {
                    retVal.Add(problem);
                }
            }

            return retVal;
        }

        //api/problem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblems()
        {
            return await _repository.GetProblems();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Problem>> GetProblem(int id)
        {
            return await _repository.GetProblem(id);
        }

        [HttpPost]
        public async Task<ActionResult<Problem>> PostProblem(Problem problem)
        {
            return await _repository.PostProblem(problem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Problem>> PutProblem(int id, Problem problem)
        {
            return await _repository.PutProblem(id, problem);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProblem(int id)
        {
            await _repository.DeleteProblem(id);
        }
    }
}

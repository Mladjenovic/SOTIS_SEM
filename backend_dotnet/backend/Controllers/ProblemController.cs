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

        [Route("ProfesorsVerbose")]
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
    }
}

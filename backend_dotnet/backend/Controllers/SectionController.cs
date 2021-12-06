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
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _repository;

        private readonly DbSotisContext _context;

        public SectionController(ISectionRepository repository, DbSotisContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Route("SectionRelatedToTest/{testId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSectionsRelatedToTest(int testId)
        {
            var sections = await _context.Sections.ToListAsync();
            var test = await _context.Tests.FindAsync(testId);

            List<Section> retVal = new List<Section>();
            foreach (var section in sections)
            {
                if (section.TestId == test.Id)
                {
                    retVal.Add(section);
                }
            }

            return retVal;
        }

        //api/problem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _repository.GetSections();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            return await _repository.GetSection(id);
        }

        [HttpPost]
        public async Task<ActionResult<Section>> PostProblem(Section section)
        {
            return await _repository.PostSection(section);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Section>> PutSection(int id, Section section)
        {
            return await _repository.PutSection(id, section);
        }

        [HttpDelete("{id}")]
        public async Task DeleteSection(int id)
        {
            await _repository.DeleteSection(id);
        }

    }
}

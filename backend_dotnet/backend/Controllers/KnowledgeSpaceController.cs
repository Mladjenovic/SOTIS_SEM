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
    public class KnowledgeSpaceController : ControllerBase
    {

        private readonly IKnowledgeSpaceRepository _repository;
        private readonly DbSotisContext _context;

        public KnowledgeSpaceController(IKnowledgeSpaceRepository repository, DbSotisContext context)
        {
            _repository = repository;
            _context = context;
        }

        //api/knowledgeSpace
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeSpace>>> GetKnowledgeSpaces()
        {
            return await _repository.GetKnowledgeSpaces();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KnowledgeSpace>> GetKnowledgeSpace(int id)
        {
            return await _repository.GetKnowledgeSpace(id);
        }

        [HttpPost]
        public async Task<ActionResult<KnowledgeSpace>> PostKnowledgeSpace(KnowledgeSpace knowledgeSpace)
        {
            return await _repository.PostKnowledgeSpace(knowledgeSpace);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<KnowledgeSpace>> PutKnowledgeSpace(int id, KnowledgeSpace knowledgeSpace)
        {
            return await _repository.PutKnowledgeSpace(id, knowledgeSpace);
        }

        [HttpDelete("{id}")]
        public async Task DeleteKnowledgeSpace(int id)
        {
            await _repository.DeleteKnowledgeSpace(id);
        }


        [Route("KnowledgeSpaceRelatedToSubject/{subjectId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeSpace>>> GetKnowledgeSpacesRelatedToSubject(int subjectId)
        {
            var knowledgeSpaces = await _context.KnowledgeSpaces.ToListAsync();
            var subject = await _context.Subjects.FindAsync(subjectId);

            List<KnowledgeSpace> retVal = new List<KnowledgeSpace>();
            foreach (var ks in knowledgeSpaces)
            {
                if (ks.SubjectId == subject.Id)
                {
                    retVal.Add(ks);
                }
            }

            return retVal;
        }
    }
}

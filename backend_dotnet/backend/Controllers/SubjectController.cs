using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repository;

        public SubjectController(ISubjectRepository repository)
        {
            _repository = repository;
        }

        //api/subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _repository.GetSubjects();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            return await _repository.GetSubject(id);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            return await _repository.PostSubject(subject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> PutSubject(int id, Subject subject)
        {
            return await _repository.PutSubject(id, subject);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTest(int id)
        {
            await _repository.DeleteSubject(id);
        }


    }
}

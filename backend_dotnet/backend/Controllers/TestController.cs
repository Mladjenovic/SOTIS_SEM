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
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _repository;
        public TestController(ITestRepository repository)
        {
            _repository = repository;
        }
        //GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            return await _repository.GetTests();
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            return await _repository.GetTest(id);
        }

        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(Test test)
        {
            return await _repository.PostTest(test);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Test>> PostTest(int id, Test test)
        {
            return await _repository.PutTest(id,test);
        }

        [HttpDelete ("{id}")]
        public async Task DeleteTest(int id)
        {
            await _repository.DeleteTest(id);
        }


    }
}

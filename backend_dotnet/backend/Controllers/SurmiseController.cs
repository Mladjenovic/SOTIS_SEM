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
    public class SurmiseController : ControllerBase
    {
        private readonly ISurmiseRepository _repository;

        public SurmiseController(ISurmiseRepository repository)
        {
            _repository = repository;
        }

        //api/surmise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sumarise>>> GetSumarises()
        {
            return await _repository.GetSurmises();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sumarise>> GetSurmise(int id)
        {
            return await _repository.GetSurmise(id);
        }

        [HttpPost]
        public async Task<ActionResult<Sumarise>> PostProblem(Sumarise sumarise)
        {
            return await _repository.PostSurmise(sumarise);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Sumarise>> PutProblem(int id, Sumarise sumarise)
        {
            return await _repository.PutSurmise(id, sumarise);
        }

        [HttpDelete("{id}")]
        public async Task DeleteSurmise(int id)
        {
            await _repository.DeleteSurmise(id);
        }
    }
}

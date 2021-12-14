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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _repository;
        private readonly DbSotisContext _context;

        public AnswerController(IAnswerRepository repository, DbSotisContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Route("AnswersRelatedToQuestion/{questionId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersRelatedToQuestion(int questionId)
        {
            var answers = await _context.Answers.ToListAsync();
            var question = await _context.Questions.FindAsync(questionId);

            List<Answer> retVal = new List<Answer>();
            foreach (var answer in answers)
            {
                if (answer.QuestionId == question.Id)
                {
                    retVal.Add(answer);
                }
            }

            return retVal;
        }

        //api/problem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            return await _repository.GetAnswers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            return await _repository.GetAnswer(id);
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            return await _repository.PostAnswer(answer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Answer>> PutProblem(int id, Answer answer)
        {
            return await _repository.PutAnswer(id, answer);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAnswer(int id)
        {
            await _repository.DeleteAnswer(id);
        }

    }
}

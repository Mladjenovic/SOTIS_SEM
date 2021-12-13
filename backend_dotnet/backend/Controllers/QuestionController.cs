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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _repository;
        private readonly DbSotisContext _context;

        public QuestionController(IQuestionRepository repository, DbSotisContext context)
        {
            _repository = repository;
            _context = context;
        }

        //api/question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _repository.GetQuestions();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            return await _repository.GetQuestion(id);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostProblem(Question question)
        {
            return await _repository.PostQuestion(question);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Question>> PutProblem(int id, Question question)
        {
            return await _repository.PutQuestion(id, question);
        }

        [HttpDelete("{id}")]
        public async Task DeleteQuestion(int id)
        {
            await _repository.DeleteQuestion(id);
        }


        [Route("QuestionsRealtedToSection/{sectionId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsRelatedToSection(int sectionId)
        {
            var questions = await _context.Questions.ToListAsync();
            var section = await _context.Sections.FindAsync(sectionId);

            List<Question> retVal = new List<Question>();
            foreach (var question in questions)
            {
                if (question.SectionId == section.Id)
                {
                    retVal.Add(question);
                }
            }

            return retVal;
        }


    }
}

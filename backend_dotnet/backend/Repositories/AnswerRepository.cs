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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DbSotisContext _context;

        public AnswerRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer != null)
            {
                _context.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var answers = await _context.Answers.ToListAsync();
            foreach (var answer  in answers)
            {
                _context.Entry(answer).Reference(item => item.Question).Load();
            }
            return answers;
        }

        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<ActionResult<Answer>> PutAnswer(int id, Answer answer)
        {
            var find = await _context.Answers.FindAsync(id);
            if (find != null)
            {
                find.Text = answer.Text;
                find.Correct = answer.Correct;
                find.QuestionId = answer.QuestionId;
            }

            return find;
        }
    }
}

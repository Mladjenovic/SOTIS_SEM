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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DbSotisContext _context;

        public QuestionRepository(DbSotisContext context)
        {
            _context = context;
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _context.Questions.ToListAsync();
            foreach (var question in questions)
            {
                _context.Entry(question).Reference(item => item.Problem).Load();
                _context.Entry(question).Reference(item => item.Section).Load();
            }
            return questions;
        }

        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            return await _context.Questions.FindAsync(id);

        }

        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<ActionResult<Question>> PutQuestion(int id, Question question)
        {
            var find = await _context.Questions.FindAsync(id);
            if (find != null)
            {
                find.Text = question.Text;
                find.PointsPerQuestion = question.PointsPerQuestion;
                find.SectionId = question.SectionId;
                find.ProblemId = question.ProblemId;
            }

            return find;
        }
    }
}

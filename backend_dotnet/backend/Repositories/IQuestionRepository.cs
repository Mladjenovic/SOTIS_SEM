using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IQuestionRepository
    {
        Task<ActionResult<IEnumerable<Question>>> GetQuestions();
        Task<ActionResult<Question>> GetQuestion(int id);
        Task<ActionResult<Question>> PostQuestion(Question question);
        Task<ActionResult<Question>> PutQuestion(int id, Question question);
        Task DeleteQuestion(int id);
    }
}

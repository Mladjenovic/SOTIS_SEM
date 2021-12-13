using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IAnswerRepository
    {
        Task<ActionResult<IEnumerable<Answer>>> GetAnswers();
        Task<ActionResult<Answer>> GetAnswer(int id);
        Task<ActionResult<Answer>> PostAnswer(Answer answer);
        Task<ActionResult<Answer>> PutAnswer(int id, Answer answer);
        Task DeleteAnswer(int id);
    }
}

using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IProblemRespository
    {
        Task<ActionResult<IEnumerable<Problem>>> GetProblems();
        Task<ActionResult<Problem>> GetProblem(int id);
        Task<ActionResult<Problem>> PostProblem(Problem problem);
        Task<ActionResult<Problem>> PutProblem(int id, Problem problem);
        Task DeleteProblem(int id);
    }
}

using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface ITestRepository
    {
        Task<ActionResult<IEnumerable<Test>>> GetTests();
        Task<ActionResult<Test>> GetTest(int id);
        Task<ActionResult<Test>> PostTest(Test test);
        Task<ActionResult<Test>> PutTest(int id, Test test);
        Task DeleteTest(int id);
    }
}

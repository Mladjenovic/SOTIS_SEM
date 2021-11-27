using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface ISubjectRepository
    {
        Task<ActionResult<IEnumerable<Subject>>> GetSubjects();
        Task<ActionResult<Subject>> GetSubject(int id); 
        Task<ActionResult<Subject>> PostSubject(Subject subject);
        Task<ActionResult<Subject>> PutSubject(int id, Subject subject);
        Task DeleteSubject(int id);
    }
}

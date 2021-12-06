using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface ISurmiseRepository
    {
        Task<ActionResult<IEnumerable<Sumarise>>> GetSurmises();
        Task<ActionResult<Sumarise>> GetSurmise(int id);
        Task<ActionResult<Sumarise>> PostSurmise(Sumarise sumarise);
        Task<ActionResult<Sumarise>> PutSurmise(int id, Sumarise sumarise);
        Task DeleteSurmise(int id);
    }
}

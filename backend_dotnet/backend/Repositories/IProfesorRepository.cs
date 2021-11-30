using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IProfesorRepository
    {
        Task<ActionResult<IEnumerable<Profesor>>> GetProfesors();
        Task<ActionResult<Profesor>> GetProfesor(int id);
        Task<ActionResult<Profesor>> PostProfesor(Profesor profesor);
        Task<ActionResult<Profesor>> PutProfesor(int id, Profesor profesor);
        Task DeleteProfesor(int id);
    }
}

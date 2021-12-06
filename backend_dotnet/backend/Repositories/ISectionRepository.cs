using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface ISectionRepository
    {
        Task<ActionResult<IEnumerable<Section>>> GetSections();
        Task<ActionResult<Section>> GetSection(int id);
        Task<ActionResult<Section>> PostSection(Section section);
        Task<ActionResult<Section>> PutSection(int id, Section section);
        Task DeleteSection(int id);
    }
}

using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IKnowledgeSpaceRepository
    {
        Task<ActionResult<IEnumerable<KnowledgeSpace>>> GetKnowledgeSpaces();
        Task<ActionResult<KnowledgeSpace>> GetKnowledgeSpace(int id);
        Task<ActionResult<KnowledgeSpace>> PostKnowledgeSpace(KnowledgeSpace knowledgeSpace);
        Task<ActionResult<KnowledgeSpace>> PutKnowledgeSpace(int id, KnowledgeSpace knowledgeSpace);
        Task DeleteKnowledgeSpace(int id);
    }
}

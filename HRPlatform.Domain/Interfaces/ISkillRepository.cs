using HRPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<Skill> GetByNameAsync(string name);
        Task<IEnumerable<Skill>> GetSkillsByNamesAsync(List<string> names);
        Task<bool> ExistsAsync(string name);
    }
}

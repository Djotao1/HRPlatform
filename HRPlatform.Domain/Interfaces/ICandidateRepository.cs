using HRPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate> GetCandidateWithSkillsAsync(int id);
        Task<IEnumerable<Candidate>> GetCandidatesWithSkillsAsync();
        Task<IEnumerable<Candidate>> SearchCandidatesAsync(string name, List<string> skills);

        // Two separate methods instead of one with optional parameter
        Task<bool> EmailExistsAsync(string email);
        Task<bool> EmailExistsAsync(string email, int excludeCandidateId);
    }
}

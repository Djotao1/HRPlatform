using HRPlatform.Application.Candidates.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateDto> CreateCandidateAsync(CreateCandidateRequest request);
        Task<CandidateDto> UpdateCandidateAsync(int id, UpdateCandidateRequest request);
        Task DeleteCandidateAsync(int id);
        Task AddSkillToCandidateAsync(int candidateId, int skillId);
        Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
        Task<IEnumerable<CandidateDto>> SearchCandidatesAsync(CandidateSearchRequest request);
        Task<CandidateDto> GetCandidateByIdAsync(int id);
    }

    
}

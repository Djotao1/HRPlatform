using HRPlatform.Application.Candidates.DTOs;
using HRPlatform.Application.Skills.DTOs;
using HRPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Application.Common
{
    public static class CandidateMappingProfile
    {
        public static CandidateDto ToDto(this Candidate candidate)
        {
            if (candidate == null) return null;

            return new CandidateDto
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                DateOfBirth = candidate.DateOfBirth,
                ContactNumber = candidate.ContactNumber,
                Email = candidate.Email?.Value,
                Skills = candidate.Skills?.Select(s => new SkillDto
                {
                    Id = s.Skill.Id,
                    Name = s.Skill.Name
                }).ToList() ?? new List<SkillDto>(),
                CreatedAt = candidate.CreatedAt
            };
        }

       

        public static List<CandidateDto> ToDto(this IEnumerable<Candidate> candidates)
        {
            return candidates?.Select(c => c.ToDto()).ToList() ?? new List<CandidateDto>();
        }

        
    }
}

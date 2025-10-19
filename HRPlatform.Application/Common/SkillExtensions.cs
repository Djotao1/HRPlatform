using HRPlatform.Application.Skills.DTOs;
using HRPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HRPlatform.Application.Common
{
    public static class SkillExtensions
    {
        public static SkillDto ToDto(this Skill skill)
        {
            if (skill == null) return null;

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                CreatedAt = skill.CreatedAt
            };
        }

        public static IEnumerable<SkillDto> ToDto(this IEnumerable<Skill> skills)
        {
            return skills?.Select(s => s.ToDto()) ?? Enumerable.Empty<SkillDto>();
        }
    }
}
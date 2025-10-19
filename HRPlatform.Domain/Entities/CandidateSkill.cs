using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Entities
{
    public class CandidateSkill
    {
        public int CandidateId { get; private set; }
        public Candidate Candidate { get; private set; }

        public int SkillId { get; private set; }
        public Skill Skill { get; private set; }

        public DateTime AddedAt { get; private set; } = DateTime.UtcNow;

        // Private constructor for EF
        private CandidateSkill() { }

        public static CandidateSkill Create(Candidate candidate, Skill skill)
        {
            if (candidate == null)
                throw new ArgumentNullException(nameof(candidate));
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));

            return new CandidateSkill
            {
                Candidate = candidate,
                CandidateId = candidate.Id,
                Skill = skill,
                SkillId = skill.Id
            };
        }
    }
}

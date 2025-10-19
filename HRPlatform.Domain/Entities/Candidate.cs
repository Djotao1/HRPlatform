using HRPlatform.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Entities
{
    public class Candidate : Entity
    {
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string ContactNumber { get; private set; }
        public Email Email { get; private set; }

        private readonly List<CandidateSkill> _skills = new();
        public IReadOnlyCollection<CandidateSkill> Skills => _skills.AsReadOnly();

        // Private constructor for EF
        private Candidate() { }

        public static Candidate Create(string fullName, DateTime dateOfBirth, string email, string contactNumber = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name is required");

            if (fullName.Length > 100)
                throw new DomainException("Full name cannot be longer than 100 characters");

            if (DateTime.Now.AddYears(-18) < dateOfBirth)
                throw new DomainException("Candidate must be at least 18 years old");

            return new Candidate
            {
                FullName = fullName.Trim(),
                DateOfBirth = dateOfBirth,
                Email = new Email(email),
                ContactNumber = contactNumber?.Trim(),
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdatePersonalInfo(string fullName, DateTime dateOfBirth, string contactNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name is required");

            if (DateTime.Now.AddYears(-18) < dateOfBirth)
                throw new DomainException("Candidate must be at least 18 years old");

            FullName = fullName.Trim();
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber?.Trim();
            UpdateTimestamps();
        }

        public void AddSkill(Skill skill)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));

            if (_skills.Any(s => s.SkillId == skill.Id))
                throw new DomainException("Skill already added to candidate");

            var candidateSkill = CandidateSkill.Create(this, skill);
            _skills.Add(candidateSkill);
            UpdateTimestamps();
        }

        public void RemoveSkill(Skill skill)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));

            var candidateSkill = _skills.FirstOrDefault(s => s.SkillId == skill.Id);
            if (candidateSkill != null)
            {
                _skills.Remove(candidateSkill);
                UpdateTimestamps();
            }
        }

        public void ClearSkills()
        {
            _skills.Clear();
            UpdateTimestamps();
        }
    }
}

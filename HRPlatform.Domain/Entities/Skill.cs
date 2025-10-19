using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Entities
{
    public class Skill : Entity
    {
        public string Name { get; private set; }

        // Navigation property
        public ICollection<CandidateSkill> CandidateSkills { get; private set; } = new List<CandidateSkill>();

        private Skill() { } // For EF Core

        private Skill(string name)
        {
            Name = name?.Trim() ?? throw new DomainException("Skill name cannot be null or empty.");
            Validate();
        }

        public static Skill Create(string name)
        {
            return new Skill(name);
        }

        // ADD THIS MISSING UPDATE METHOD
        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Skill name cannot be null or empty.");

            Name = name.Trim();
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new DomainException("Skill name cannot be empty.");

            if (Name.Length > 100)
                throw new DomainException("Skill name cannot exceed 100 characters.");
        }

        // Optional: Override equality members for better entity comparison
        public override bool Equals(object obj)
        {
            if (obj is not Skill other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Id != 0 && other.Id != 0)
                return Id == other.Id;

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}


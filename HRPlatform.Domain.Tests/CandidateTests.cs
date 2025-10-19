using HRPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Tests
{
    public class CandidateTests
    {
        [Fact]
        public void Create_ValidData_ReturnsCandidate()
        {
            // Arrange
            var fullName = "John Doe";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var email = "john.doe@example.com";
            var contactNumber = "+1234567890";

            // Act
            var candidate = Candidate.Create(fullName, dateOfBirth, email, contactNumber);

            // Assert
            Assert.NotNull(candidate);
            Assert.Equal(fullName, candidate.FullName);
            Assert.Equal(dateOfBirth, candidate.DateOfBirth);
            Assert.Equal(email, candidate.Email.Value);
            Assert.Equal(contactNumber, candidate.ContactNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Create_InvalidFullName_ThrowsDomainException(string invalidFullName)
        {
            // Arrange
            var dateOfBirth = new DateTime(1990, 1, 1);
            var email = "john.doe@example.com";
            var contactNumber = "+1234567890";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                Candidate.Create(invalidFullName, dateOfBirth, email, contactNumber));
        }

        [Fact]
        public void Create_InvalidEmail_ThrowsDomainException()
        {
            // Arrange
            var fullName = "John Doe";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var invalidEmail = "invalid-email";
            var contactNumber = "+1234567890";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                Candidate.Create(fullName, dateOfBirth, invalidEmail, contactNumber));
        }

        [Fact]
        public void AddSkill_ValidSkill_AddsSkillToCandidate()
        {
            // Arrange
            var candidate = Candidate.Create("John Doe", new DateTime(1990, 1, 1), "john@example.com", "+1234567890");
            var skill = Skill.Create("C# programming");

            // Act
            candidate.AddSkill(skill);

            // Assert
            Assert.Single(candidate.Skills);
            Assert.Equal(skill, candidate.Skills.First().Skill);
        }

        [Fact]
        public void AddSkill_DuplicateSkill_ThrowsDomainException()
        {
            // Arrange
            var candidate = Candidate.Create("John Doe", new DateTime(1990, 1, 1), "john@example.com", "+1234567890");
            var skill = Skill.Create("C# programming");
            candidate.AddSkill(skill);

            // Act & Assert
            Assert.Throws<DomainException>(() => candidate.AddSkill(skill));
        }

        [Fact]
        public void RemoveSkill_ExistingSkill_RemovesSkill()
        {
            // Arrange
            var candidate = Candidate.Create("John Doe", new DateTime(1990, 1, 1), "john@example.com", "+1234567890");
            var skill = Skill.Create("C# programming");
            candidate.AddSkill(skill);

            // Act
            candidate.RemoveSkill(skill);

            // Assert
            Assert.Empty(candidate.Skills);
        }

        [Fact]
        public void UpdatePersonalInfo_ValidData_UpdatesProperties()
        {
            // Arrange
            var candidate = Candidate.Create("John Doe", new DateTime(1990, 1, 1), "john@example.com", "+1234567890");
            var newName = "Jane Smith";
            var newDob = new DateTime(1995, 5, 5);
            var newContact = "+0987654321";

            // Act
            candidate.UpdatePersonalInfo(newName, newDob, newContact);

            // Assert
            Assert.Equal(newName, candidate.FullName);
            Assert.Equal(newDob, candidate.DateOfBirth);
            Assert.Equal(newContact, candidate.ContactNumber);
        }
    }
}

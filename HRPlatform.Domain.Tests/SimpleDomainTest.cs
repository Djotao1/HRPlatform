using HRPlatform.Domain.Entities;
using HRPlatform.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Tests
{
    public class SimpleDomainTest
    {
        [Fact]
        public void Can_Create_Candidate_With_Valid_Data()
        {
            // Arrange & Act
            var candidate = Candidate.Create(
                "John Doe",
                new DateTime(1990, 1, 1),
                "john.doe@example.com",
                "+1234567890");

            // Assert
            Assert.Equal("John Doe", candidate.FullName);
            Assert.Equal("john.doe@example.com", candidate.Email.Value);
        }

        [Fact]
        public void Can_Create_Skill_With_Valid_Name()
        {
            // Arrange & Act
            var skill = Skill.Create("C# Programming");

            // Assert
            Assert.Equal("C# Programming", skill.Name);
        }

        [Fact]
        public void Email_ValueObject_Validates_Correctly()
        {
            // Arrange & Act
            var email = new Email("test@example.com");

            // Assert
            Assert.Equal("test@example.com", email.Value);
        }
    }
}

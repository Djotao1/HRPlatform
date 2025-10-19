using HRPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Tests
{
    public class SkillTests
    {
        [Fact]
        public void Create_ValidName_ReturnsSkill()
        {
            // Arrange
            var skillName = "C# programming";

            // Act
            var skill = Skill.Create(skillName);

            // Assert
            Assert.NotNull(skill);
            Assert.Equal(skillName, skill.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Create_InvalidName_ThrowsDomainException(string invalidName)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => Skill.Create(invalidName));
        }

        [Fact]
        public void Create_NameTooLong_ThrowsDomainException()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<DomainException>(() => Skill.Create(longName));
        }

        [Fact]
        public void Update_ValidName_UpdatesSkill()
        {
            // Arrange
            var skill = Skill.Create("Old Name");
            var newName = "New Skill Name";

            // Act
            skill.Update(newName);

            // Assert
            Assert.Equal(newName, skill.Name);
        }
    }
}

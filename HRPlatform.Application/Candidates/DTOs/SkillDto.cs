using System;
using System.Collections.Generic;

namespace HRPlatform.Application.Skills.DTOs
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateSkillRequest
    {
        public string Name { get; set; }
    }

    public class UpdateSkillRequest
    {
        public string Name { get; set; }
    }
}
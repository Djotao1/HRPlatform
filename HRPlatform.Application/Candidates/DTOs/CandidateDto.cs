using HRPlatform.Application.Skills.DTOs;
using System;
using System.Collections.Generic;

namespace HRPlatform.Application.Candidates.DTOs
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<SkillDto> Skills { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCandidateRequest
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<string> Skills { get; set; } = new();
    }

    public class UpdateCandidateRequest
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public List<string> Skills { get; set; } = new();
    }

    public class CandidateSearchRequest
    {
        public string Name { get; set; }
        public List<string> Skills { get; set; } = new();
    }
}
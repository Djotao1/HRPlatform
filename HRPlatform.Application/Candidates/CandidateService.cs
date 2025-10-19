using HRPlatform.Application.Candidates.DTOs;
using HRPlatform.Application.Common;
using HRPlatform.Application.Interfaces;
using HRPlatform.Domain;
using HRPlatform.Domain.Entities;
using HRPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Application.Candidates
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateService(
            ICandidateRepository candidateRepository,
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CandidateDto> CreateCandidateAsync(CreateCandidateRequest request)
        {
            // Check if email already exists
            if (await _candidateRepository.EmailExistsAsync(request.Email))
            {
                throw new DomainException($"Candidate with email '{request.Email}' already exists.");
            }

            var candidate = Candidate.Create(
                request.FullName,
                request.DateOfBirth,
                request.Email,
                request.ContactNumber);

            // Add skills
            if (request.Skills != null && request.Skills.Any())
            {
                var skills = await _skillRepository.GetSkillsByNamesAsync(request.Skills);
                foreach (var skill in skills)
                {
                    candidate.AddSkill(skill);
                }
            }

            await _candidateRepository.AddAsync(candidate);
            await _unitOfWork.CommitAsync();

            return candidate.ToDto();
        }

        public async Task<CandidateDto> UpdateCandidateAsync(int id, UpdateCandidateRequest request)
        {
            var candidate = await _candidateRepository.GetCandidateWithSkillsAsync(id);
            if (candidate == null)
            {
                throw new DomainException($"Candidate with ID {id} not found.");
            }

            // Check if email exists for other candidates
            if (await _candidateRepository.EmailExistsAsync(candidate.Email.Value, id))
            {
                throw new DomainException($"Candidate with email '{candidate.Email.Value}' already exists.");
            }

            candidate.UpdatePersonalInfo(request.FullName, request.DateOfBirth, request.ContactNumber);

            // Update skills
            candidate.ClearSkills();
            if (request.Skills != null && request.Skills.Any())
            {
                var skills = await _skillRepository.GetSkillsByNamesAsync(request.Skills);
                foreach (var skill in skills)
                {
                    candidate.AddSkill(skill);
                }
            }

            await _candidateRepository.UpdateAsync(candidate);
            await _unitOfWork.CommitAsync();

            return candidate.ToDto();
        }
       

        public async Task DeleteCandidateAsync(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                throw new DomainException($"Candidate with ID {id} not found.");
            }

            await _candidateRepository.DeleteAsync(candidate);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            var candidate = await _candidateRepository.GetCandidateWithSkillsAsync(candidateId);
            if (candidate == null)
            {
                throw new DomainException($"Candidate with ID {candidateId} not found.");
            }

            var skill = await _skillRepository.GetByIdAsync(skillId);
            if (skill == null)
            {
                throw new DomainException($"Skill with ID {skillId} not found.");
            }

            candidate.AddSkill(skill);
            await _candidateRepository.UpdateAsync(candidate);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var candidate = await _candidateRepository.GetCandidateWithSkillsAsync(candidateId);
            if (candidate == null)
            {
                throw new DomainException($"Candidate with ID {candidateId} not found.");
            }

            var skill = await _skillRepository.GetByIdAsync(skillId);
            if (skill == null)
            {
                throw new DomainException($"Skill with ID {skillId} not found.");
            }

            candidate.RemoveSkill(skill);
            await _candidateRepository.UpdateAsync(candidate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CandidateDto>> SearchCandidatesAsync(CandidateSearchRequest request)
        {
            var candidates = await _candidateRepository.SearchCandidatesAsync(request.Name, request.Skills);
            return candidates.ToDto();
        }

        public async Task<CandidateDto> GetCandidateByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetCandidateWithSkillsAsync(id);
            if (candidate == null)
            {
                throw new DomainException($"Candidate with ID {id} not found.");
            }

            return candidate.ToDto();
        }
    }
}

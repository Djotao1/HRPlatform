using HRPlatform.Application.Common;
using HRPlatform.Application.Interfaces;
using HRPlatform.Application.Skills.DTOs;
using HRPlatform.Domain;
using HRPlatform.Domain.Entities;
using HRPlatform.Domain.Interfaces;

namespace HRPlatform.Application.Skills
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SkillDto> CreateSkillAsync(CreateSkillRequest request)
        {
            if (await _skillRepository.ExistsAsync(request.Name))
            {
                throw new DomainException($"Skill with name '{request.Name}' already exists.");
            }

            var skill = Skill.Create(request.Name);
            await _skillRepository.AddAsync(skill);
            await _unitOfWork.CommitAsync();

            return skill.ToDto();
        }

        public async Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillRequest request)
        {
            var skill = await _skillRepository.GetByIdAsync(id);
            if (skill == null)
            {
                throw new DomainException($"Skill with ID {id} not found.");
            }

            // Check if another skill already has this name (excluding current skill)
            var existingSkill = await _skillRepository.GetByNameAsync(request.Name);
            if (existingSkill != null && existingSkill.Id != id)
            {
                throw new DomainException($"Skill with name '{request.Name}' already exists.");
            }

            // This will now work with the Update method added above
            skill.Update(request.Name);
            await _skillRepository.UpdateAsync(skill);
            await _unitOfWork.CommitAsync();

            return skill.ToDto();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _skillRepository.GetByIdAsync(id);
            if (skill == null)
            {
                throw new DomainException($"Skill with ID {id} not found.");
            }

            await _skillRepository.DeleteAsync(skill);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _skillRepository.GetAllAsync();
            return skills.ToDto();
        }

        public async Task<SkillDto> GetSkillByIdAsync(int id)
        {
            var skill = await _skillRepository.GetByIdAsync(id);
            if (skill == null)
            {
                throw new DomainException($"Skill with ID {id} not found.");
            }

            return skill.ToDto();
        }

        public async Task<SkillDto> GetSkillByNameAsync(string name)
        {
            var skill = await _skillRepository.GetByNameAsync(name);
            if (skill == null)
            {
                throw new DomainException($"Skill with name '{name}' not found.");
            }

            return skill.ToDto();
        }
    }
}
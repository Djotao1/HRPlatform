using HRPlatform.Application.Skills.DTOs;

namespace HRPlatform.Application.Interfaces
{
    public interface ISkillService
    {
        Task<SkillDto> CreateSkillAsync(CreateSkillRequest request);
        Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillRequest request);
        Task DeleteSkillAsync(int id);
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto> GetSkillByIdAsync(int id);
        Task<SkillDto> GetSkillByNameAsync(string name);
    }
}

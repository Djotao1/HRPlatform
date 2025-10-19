using HRPlatform.Application.Skills.DTOs;

namespace HRPlatform.Web.Blazor.Services
{
    public interface ISkillApiService
    {
        Task<List<SkillDto>> GetSkillsAsync();
        Task<SkillDto> GetSkillAsync(int id);
        Task<SkillDto> CreateSkillAsync(CreateSkillRequest request);
        Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillRequest request);
        Task DeleteSkillAsync(int id);
    }

    public class SkillApiService : ISkillApiService
    {
        private readonly HttpClient _httpClient;

        public SkillApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SkillDto>> GetSkillsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SkillDto>>("api/skills") ?? new List<SkillDto>();
        }

        public async Task<SkillDto> GetSkillAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SkillDto>($"api/skills/{id}");
        }

        public async Task<SkillDto> CreateSkillAsync(CreateSkillRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/skills", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkillDto>();
        }

        public async Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/skills/{id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkillDto>();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/skills/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
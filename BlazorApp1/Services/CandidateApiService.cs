using HRPlatform.Application.Candidates.DTOs;

namespace HRPlatform.Web.Blazor.Services
{
    public interface ICandidateApiService
    {
        Task<List<CandidateDto>> GetCandidatesAsync();
        Task<CandidateDto> GetCandidateAsync(int id);
        Task<CandidateDto> CreateCandidateAsync(CreateCandidateRequest request);
        Task<CandidateDto> UpdateCandidateAsync(int id, UpdateCandidateRequest request);
        Task DeleteCandidateAsync(int id);
        Task<List<CandidateDto>> SearchCandidatesAsync(string name, List<string> skills);
        Task AddSkillToCandidateAsync(int candidateId, int skillId);
        Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }

    public class CandidateApiService : ICandidateApiService
    {
        private readonly HttpClient _httpClient;

        public CandidateApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CandidateDto>> GetCandidatesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CandidateDto>>("api/candidates") ?? new List<CandidateDto>();
        }

        public async Task<CandidateDto> GetCandidateAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CandidateDto>($"api/candidates/{id}");
        }

        public async Task<CandidateDto> CreateCandidateAsync(CreateCandidateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/candidates", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CandidateDto>();
        }

        public async Task<CandidateDto> UpdateCandidateAsync(int id, UpdateCandidateRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/candidates/{id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CandidateDto>();
        }

        public async Task DeleteCandidateAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/candidates/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<CandidateDto>> SearchCandidatesAsync(string name, List<string> skills)
        {
            var skillParam = skills != null && skills.Any() ? string.Join(",", skills) : "";
            return await _httpClient.GetFromJsonAsync<List<CandidateDto>>($"api/candidates/search?name={name}&skills={skillParam}") ?? new List<CandidateDto>();
        }

        public async Task AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            var response = await _httpClient.PostAsync($"api/candidates/{candidateId}/skills/{skillId}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var response = await _httpClient.DeleteAsync($"api/candidates/{candidateId}/skills/{skillId}");
            response.EnsureSuccessStatusCode();
        }
    }
}

using HRPlatform.Application.Candidates.DTOs;
using HRPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// Get all candidates with their skills
        /// </summary>
        /// <returns>List of all candidates</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CandidateDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetAllCandidates()
        {
            var candidates = await _candidateService.SearchCandidatesAsync(new CandidateSearchRequest());
            return Ok(candidates);
        }

        /// <summary>
        /// Get candidate by ID
        /// </summary>
        /// <param name="id">Candidate ID</param>
        /// <returns>Candidate details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidateDto>> GetCandidateById(int id)
        {
            try
            {
                var candidate = await _candidateService.GetCandidateByIdAsync(id);
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Search candidates by name and/or skills
        /// </summary>
        /// <param name="name">Candidate name (partial match)</param>
        /// <param name="skills">List of skill names</param>
        /// <returns>Matching candidates</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<CandidateDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> SearchCandidates(
            [FromQuery] string name = null,
            [FromQuery] List<string> skills = null)
        {
            var request = new CandidateSearchRequest
            {
                Name = name,
                Skills = skills ?? new List<string>()
            };

            var candidates = await _candidateService.SearchCandidatesAsync(request);
            return Ok(candidates);
        }

        /// <summary>
        /// Create a new candidate
        /// </summary>
        /// <param name="request">Candidate creation data</param>
        /// <returns>Created candidate</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CandidateDto>> CreateCandidate([FromBody] CreateCandidateRequest request)
        {
            try
            {
                var candidate = await _candidateService.CreateCandidateAsync(request);
                return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.Id }, candidate);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing candidate
        /// </summary>
        /// <param name="id">Candidate ID</param>
        /// <param name="request">Candidate update data</param>
        /// <returns>Updated candidate</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidateDto>> UpdateCandidate(int id, [FromBody] UpdateCandidateRequest request)
        {
            try
            {
                var candidate = await _candidateService.UpdateCandidateAsync(id, request);
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(new { error = ex.Message });

                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a candidate
        /// </summary>
        /// <param name="id">Candidate ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            try
            {
                await _candidateService.DeleteCandidateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Add a skill to a candidate
        /// </summary>
        /// <param name="candidateId">Candidate ID</param>
        /// <param name="skillId">Skill ID</param>
        [HttpPost("{candidateId}/skills/{skillId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddSkillToCandidate(int candidateId, int skillId)
        {
            try
            {
                await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
                return Ok(new { message = "Skill added to candidate successfully" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(new { error = ex.Message });

                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Remove a skill from a candidate
        /// </summary>
        /// <param name="candidateId">Candidate ID</param>
        /// <param name="skillId">Skill ID</param>
        [HttpDelete("{candidateId}/skills/{skillId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSkillFromCandidate(int candidateId, int skillId)
        {
            try
            {
                await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
                return Ok(new { message = "Skill removed from candidate successfully" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(new { error = ex.Message });

                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
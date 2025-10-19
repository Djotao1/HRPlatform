using HRPlatform.Application.Interfaces;
using HRPlatform.Application.Skills.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HRPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /// <summary>
        /// Get all skills
        /// </summary>
        /// <returns>List of all skills</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SkillDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        /// <summary>
        /// Get skill by ID
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <returns>Skill details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDto>> GetSkillById(int id)
        {
            try
            {
                var skill = await _skillService.GetSkillByIdAsync(id);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get skill by name
        /// </summary>
        /// <param name="name">Skill name</param>
        /// <returns>Skill details</returns>
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDto>> GetSkillByName(string name)
        {
            try
            {
                var skill = await _skillService.GetSkillByNameAsync(name);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new skill
        /// </summary>
        /// <param name="request">Skill creation data</param>
        /// <returns>Created skill</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] CreateSkillRequest request)
        {
            try
            {
                var skill = await _skillService.CreateSkillAsync(request);
                return CreatedAtAction(nameof(GetSkillById), new { id = skill.Id }, skill);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing skill
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="request">Skill update data</param>
        /// <returns>Updated skill</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDto>> UpdateSkill(int id, [FromBody] UpdateSkillRequest request)
        {
            try
            {
                var skill = await _skillService.UpdateSkillAsync(id, request);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(new { error = ex.Message });

                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a skill
        /// </summary>
        /// <param name="id">Skill ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                await _skillService.DeleteSkillAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
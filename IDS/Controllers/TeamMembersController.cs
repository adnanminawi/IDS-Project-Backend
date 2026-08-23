using IDS.Models.Dtos;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _service;

        public TeamMembersController(ITeamMemberService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teamMembers = await _service.GetAllAsync();
            return Ok(teamMembers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var teamMember = await _service.GetByIdAsync(id);
            if (teamMember == null)
                return NotFound();
            return Ok(teamMember);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamMemberDto dto)
        {
            var newId = _service.CreateAsync(dto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateTeamMemberDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

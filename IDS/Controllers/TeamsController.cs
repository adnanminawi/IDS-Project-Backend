using IDS.Models.Dtos;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamsController(ITeamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _service.GetAllAsync();
            return Ok(teams);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _service.GetByIdAsync(id);
            if (team == null)
                return NotFound();
            return Ok(team);
        }
        [HttpGet("{teamId}/members")]
        public async Task<IActionResult> GetMembers(int teamId)
        {
            var members = await _service.GetTeamMembersAsync(teamId);
            return Ok(members);
        }
        //POST

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamDto dto)
        {
            var newId = await _service.CreateAsync(dto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateTeamDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if(!success)
                return NotFound();

            return NoContent();
        }

        //DELETE
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

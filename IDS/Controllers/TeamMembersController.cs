using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _service;
        private readonly IMemoryCache _cache;

        public TeamMembersController(ITeamMemberService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "all_teammembers";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<TeamMember>? teamMembers))
            {
                Debug.WriteLine(">>> CACHE MISS — hitting database");   // ← only prints on DB fetch
                teamMembers = await _service.GetAllAsync();
                _cache.Set(cacheKey, teamMembers, TimeSpan.FromMinutes(5));
            }
            else
            {
                Debug.WriteLine(">>> CACHE HIT — served from cache");// ← prints when cached
            }
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
            var newId = await _service.CreateAsync(dto);   
            _cache.Remove("all_teammembers");              
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateTeamMemberDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            _cache.Remove("all_teammembers");              
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                _cache.Remove("all_teammembers");          
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

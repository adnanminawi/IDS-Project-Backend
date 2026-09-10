using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeploymentsController : ControllerBase
    {
        private readonly IDeploymentService _service;

        public DeploymentsController(IDeploymentService service)
        {
            _service = service;
        }
        private bool CanManageDeployments()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var position = User.FindFirst("Position")?.Value;
            return role == "Admin" || position == "CEO" || position == "Manager" || position == "Project Manager";
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var position = User.FindFirst("Position")?.Value;
            var teamIdClaim = User.FindFirst("TeamId")?.Value;
            int? teamId = teamIdClaim != null ? int.Parse(teamIdClaim) : null;

            var deployments = await _service.GetDeploymentsByTeamAsync(position, teamId);
            return Ok(deployments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var deployment = await _service.GetByIdAsync(id);
            if (deployment == null)
            {
                return NotFound();
            }
            return Ok(deployment);
        }
        [HttpGet("{deploymentId}/environments")]
        public async Task<IActionResult> GetEnvironmentsByDeployment(int deploymentId)
        {
            var environments = await _service.GetEnvironmentsByDeploymentIdAsync(deploymentId);
            
            if (environments == null)
                return NotFound();

            return Ok(environments);
        }

        [HttpGet("{deploymentId}/modules")]
        public async Task<IActionResult> GetModulesByDeployment(int deploymentId)
        {
            var modules = await _service.GetModulesByDeploymentIdAsync(deploymentId);
            
            if (modules == null)
                return NotFound();
            
            return Ok(modules);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeploymentDto dto)
        {
            if (!CanManageDeployments())
                return Forbid();
            var newId = await _service.CreateAsync(dto);
            return Created();
        }

        //PUT

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDeploymentDto dto)
        {
            if (!CanManageDeployments())
                return Forbid();
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!CanManageDeployments())
                return Forbid();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(); 

                return NoContent();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

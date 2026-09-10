using IDS.Models.Dtos;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService clientService)
        {
            _service = clientService;
        }
        private bool CanManageClients()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var position = User.FindFirst("Position")?.Value;
            return role == "Admin" || position == "Manager" || position == "CEO" || position == "Project Manager";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var position = User.FindFirst("Position")?.Value;
            var teamIdClaim = User.FindFirst("TeamId")?.Value;
            int? teamId = teamIdClaim != null ? int.Parse(teamIdClaim) : null;

            var clients = await _service.GetClientsByTeamAsync(position, teamId);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var client = await _service.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [HttpGet("{clientId}/deployments")]
        public async Task<IActionResult> GetDeployments(int clientId)
        {
            var deployments = await _service.GetDeploymentsByClientAsync(clientId);
            
            return Ok(deployments);
        }

        //POST

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            if (!CanManageClients())
                return Forbid();

            var newId = await _service.CreateAsync(dto);
            return Created();
        }

        //PUT

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateClientDto dto)
        {
            if (!CanManageClients())
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
            if (!CanManageClients())
                return Forbid();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using IDS.Models.Dtos;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService clientService)
        {
            _service = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _service.GetAllAsync();
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
            var newId= await _service.CreateAsync(dto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateClientDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}

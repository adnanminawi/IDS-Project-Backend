using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IDeploymentService _deploymentService;
        private readonly ITeamService _teamService;

        public DashboardController(IProductService productService, IClientService clientService, IDeploymentService deploymentService, ITeamService teamService)
        {
            _productService = productService;
            _clientService = clientService;
            _deploymentService = deploymentService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var products = await _productService.GetAllAsync();
            var clients = await _clientService.GetAllAsync();
            var deployments = await _deploymentService.GetAllAsync();
            var teams = await _teamService.GetAllAsync();

            var stats = new
            {
                TotalProducts = products.Count(),
                TotalClients = clients.Count(),
                TotalDeployments = deployments.Count(),
                TotalTeams = teams.Count()
            };

            return Ok(stats);
        }
    }
}

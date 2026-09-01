using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);

        }
        [HttpGet("{productId}/modules")]
        public async Task<IActionResult> GetModulesByProduct(int productId)
        {
            var module = await _service.GetModulesByProductAsync(productId);

            return Ok(module);
        }
        [HttpGet("{productId}/responsibilities")]
        public async Task<IActionResult> GetResponsibilitiesByProduct(int productId)
        {
            var responsibility = await _service.GetResponsibilitiesByProductAsync(productId);

            return Ok(responsibility);
        }
        [HttpGet("{productId}/documentations")]
        public async Task<IActionResult> GetDocumentationByProductAsync(int productId)
        {
            var documentation = await _service.GetDocumentationByProductAsync(productId);


            return Ok(documentation);
        }
        [HttpGet("{productId}/repositories")]
        public async Task<IActionResult> GetRepositoriesByProductAsync(int productId)
        {
            var repository = await _service.GetRepositoriesByProductAsync(productId);

            return Ok(repository);
        }
        [HttpGet("{productId}/deployments")]
        public async Task<IActionResult> GetDeployments(int productId)
        {
            var deployments = await _service.GetDeploymentsByProductAsync(productId);

            return Ok(deployments);
        }


        //POST

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var newId = await _service.CreateAsync(dto);
            return Created();
        }


        [HttpPost("{productId}/responsibilities")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddResponsibility(int productId, CreateResponsibilityDto dto)
        {
            var newId = await _service.CreateResponsibilityAsync(productId, dto);
            return Created();
        }

        [HttpPost("{productId}/modules")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddModule(int productId, CreateModuleDto dto)
        {
            var newId = await _service.CreateModuleAsync(productId, dto);
            return Created();
        }

        //PUT

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }


        //Delete

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
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

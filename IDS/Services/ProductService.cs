using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;

namespace IDS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
         public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Module>> GetModulesByProductAsync(int productId)
        {
            return await _repository.GetModulesByProductAsync(productId);
        }
        public async Task<IEnumerable<Responsibility>> GetResponsibilitiesByProductAsync(int productId)
        {
            return await _repository.GetResponsibilitiesByProductAsync(productId);
        }
        public async Task<IEnumerable<Documentation>> GetDocumentationByProductAsync(int productId)
        {
            return await _repository.GetDocumentationByProductAsync(productId);
        }
        public async Task<IEnumerable<Repository>> GetRepositoriesByProductAsync(int productId)
        {
            return await _repository.GetRepositoriesByProductAsync(productId);
        }
        public async Task<IEnumerable<Deployment>> GetDeploymentsByProductAsync(int productId)
        {
            return await _repository.GetDeploymentsByProductAsync(productId);
        }
        public async Task<int> CreateAsync(CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Product Name is required.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Purpose = dto.Purpose,
                Status = dto.Status,
                Version = dto.Version,
                Markets = dto.Markets,
                Criticality = dto.Criticality,
                Technologies = dto.Technologies,
                Notes = dto.Notes
            };
            return await _repository.CreateAsync(product);
        }
        public async Task<bool> UpdateAsync(int id, CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Product Name is required.");

            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;


            existingProduct.Name = dto.Name;
            existingProduct.Description = dto.Description;
            existingProduct.Purpose = dto.Purpose;
            existingProduct.Status = dto.Status;
            existingProduct.Version = dto.Version;
            existingProduct.Markets = dto.Markets;
            existingProduct.Criticality = dto.Criticality;
            existingProduct.Technologies = dto.Technologies;
            existingProduct.Notes = dto.Notes;
            return await _repository.UpdateAsync(existingProduct);
        }
    }
}
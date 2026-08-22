using IDS.Models.Dtos;
using IDS.Models.Entities;

namespace IDS.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Module>> GetModulesByProductAsync(int productId);
        Task<IEnumerable<Responsibility>> GetResponsibilitiesByProductAsync(int productId);
        Task<IEnumerable<Documentation>> GetDocumentationByProductAsync(int productId);
        Task<IEnumerable<Repository>> GetRepositoriesByProductAsync(int productId); 
        Task<IEnumerable<Deployment>> GetDeploymentsByProductAsync(int productId);
        Task<int> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync (int id, CreateProductDto dto);
    }
}

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
        Task<IEnumerable<Product>> GetProductsByTeamAsync(string? position, int? teamId);
        Task<int> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync (int id, CreateProductDto dto);
        Task<bool> DeleteAsync(int id);
        Task<int> CreateResponsibilityAsync(int productId, CreateResponsibilityDto dto);
        Task<int> CreateModuleAsync(int productId, CreateModuleDto dto);
    }
}

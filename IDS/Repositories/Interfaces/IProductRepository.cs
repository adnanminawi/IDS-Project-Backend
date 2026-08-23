using IDS.Models.Entities;

namespace IDS.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Module>> GetModulesByProductAsync(int productId);
        Task<IEnumerable<Responsibility>> GetResponsibilitiesByProductAsync(int productId);
        Task<IEnumerable<Documentation>> GetDocumentationByProductAsync(int productId);
        Task<IEnumerable<Repository>> GetRepositoriesByProductAsync(int productId);
        Task<IEnumerable<Deployment>> GetDeploymentsByProductAsync(int productId);
        Task<int> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);


    }
}

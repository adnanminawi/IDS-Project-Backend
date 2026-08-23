using IDS.Models.Dtos;
using IDS.Models.Entities;
using Environment = IDS.Models.Entities.Environment;
namespace IDS.Services.Interfaces
{
    public interface IDeploymentService
    {
        Task<IEnumerable<Deployment>> GetAllAsync();
        Task<Deployment?> GetByIdAsync(int id);
        Task<IEnumerable<Environment?>> GetEnvironmentsByDeploymentIdAsync(int deploymentId);
        Task<IEnumerable<Module?>> GetModulesByDeploymentIdAsync(int deploymentId);
        Task<int> CreateAsync(CreateDeploymentDto dto);
        Task<bool> UpdateAsync(int id, CreateDeploymentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

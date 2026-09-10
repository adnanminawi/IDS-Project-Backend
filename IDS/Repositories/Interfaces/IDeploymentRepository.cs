using IDS.Models.Entities;
using Environment = IDS.Models.Entities.Environment;
namespace IDS.Repositories.Interfaces
{
    public interface IDeploymentRepository
    {
        Task<IEnumerable<Deployment>> GetAllAsync();
        Task<Deployment?> GetByIdAsync(int id);
        Task<IEnumerable<Environment>> GetEnvironmentsByDeploymentIdAsync(int deploymentId);
        Task<IEnumerable<Module>> GetModulesByDeploymentIdAsync(int deploymentId);
        Task<IEnumerable<Deployment>> GetDeploymentsByTeamAsync(int teamId);
        Task<int> CreateAsync (Deployment deployment);
        Task<bool> UpdateAsync(Deployment deployment);
        Task<bool> DeleteAsync(int id);
    }
}

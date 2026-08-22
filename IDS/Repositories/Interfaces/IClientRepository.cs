using IDS.Models.Entities;

namespace IDS.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<IEnumerable<Deployment>> GetDeploymentsByClientAsync(int clientId);
        Task<int> CreateAsync(Client client);
        Task<bool> UpdateAsync(Client client);
    }
}

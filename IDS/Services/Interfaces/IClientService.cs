using IDS.Models.Dtos;
using IDS.Models.Entities;

namespace IDS.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<IEnumerable<Deployment>> GetDeploymentsByClientAsync(int clientId);
        Task<int> CreateAsync(CreateClientDto dto);
        Task<bool> UpdateAsync(int id, CreateClientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

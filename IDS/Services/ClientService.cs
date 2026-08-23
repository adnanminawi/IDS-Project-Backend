using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;

namespace IDS.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Deployment>> GetDeploymentsByClientAsync(int clientId)
        {
            return await _repository.GetDeploymentsByClientAsync(clientId);
        }
        public async Task<int> CreateAsync(CreateClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Client name is required.");

            var client = new Client
            {
                Name = dto.Name,
                Country = dto.Country,
                Contact = dto.Contact,
                Status = dto.Status,
                Notes = dto.Notes
            };
            return await _repository.CreateAsync(client);
        }
        public async Task<bool> UpdateAsync(int id , CreateClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Client Name is required.");
            var existingClient = await _repository.GetByIdAsync(id);
            if (existingClient == null)
                return false;

            existingClient.Name = dto.Name;
            existingClient.Country = dto.Country;
            existingClient.Contact = dto.Contact;
            existingClient.Status = dto.Status;
            existingClient.Notes = dto.Notes;

            return await _repository.UpdateAsync(existingClient);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null)
                return false;

            var deployments = await _repository.GetDeploymentsByClientAsync(id);
            if(deployments.Any())
                throw new InvalidOperationException("Cannot delete a client that has deployments.");
            return await _repository.DeleteAsync(id);
        }
    }
}

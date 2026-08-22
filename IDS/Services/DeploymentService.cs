using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;
using Environment = IDS.Models.Entities.Environment;
namespace IDS.Services
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IDeploymentRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        public DeploymentService(IDeploymentRepository repository,IClientRepository clientRepository, IProductRepository productRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Deployment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Deployment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Environment>> GetEnvironmentsByDeploymentIdAsync(int deploymentId)
        {
            return await _repository.GetEnvironmentsByDeploymentIdAsync(deploymentId);
        }
        public async Task<IEnumerable<Module>> GetModulesByDeploymentIdAsync(int deploymentId)
        {
            return await _repository.GetModulesByDeploymentIdAsync(deploymentId);
        }
        public async Task<int> CreateAsync(CreateDeploymentDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(dto.Client_id);
            if (client == null)
                throw new ArgumentException("Client does not exist.");

            var porduct = await _productRepository.GetByIdAsync(dto.Product_id);
            if(porduct == null)
                throw new ArgumentException("Product does not exist.");

            var deployment = new Deployment
            {
                Client_id = dto.Client_id,
                Product_id = dto.Product_id,
                Version = dto.Version,
                GoLiveDate = dto.GoLiveDate,
                Status = dto.Status,
                SupportTier = dto.SupportTier,
                ClientNotes = dto.ClientNotes,
            };
            return await _repository.CreateAsync(deployment);
        }
        public async Task<bool> UpdateAsync(int id, CreateDeploymentDto dto)
        {
            var existingDeployment = await _repository.GetByIdAsync(id);
            if (existingDeployment == null)
                return false;

            var client = await _clientRepository.GetByIdAsync(dto.Client_id);
            if (client == null)
                throw new ArgumentException("Client does not exist.");

            var product = await _productRepository.GetByIdAsync(dto.Product_id);
            if (product == null)
                throw new ArgumentException("Product does not exist.");

            existingDeployment.Client_id = dto.Client_id;
            existingDeployment.Product_id = dto.Product_id;
            existingDeployment.Version = dto.Version;
            existingDeployment.GoLiveDate = dto.GoLiveDate;
            existingDeployment.Status = dto.Status;
            existingDeployment.SupportTier = dto.SupportTier;
            existingDeployment.ClientNotes = dto.ClientNotes;

            return await _repository.UpdateAsync(existingDeployment);
        }
    }
}
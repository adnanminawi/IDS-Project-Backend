using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;
using IDS.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
namespace IDS.Services
{
    public class TeamService : ITeamService
    {

        private readonly ITeamRepository _repository;

        public TeamService (ITeamRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> CreateAsync(CreateTeamDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Team name is required.");
            var team = new Team
            {
                Name = dto.Name
            };
            return await _repository.CreateAsync(team);
        }
        public async Task<bool> UpdateAsync(int id, CreateTeamDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Team Name is required.");

            var existingTeam = await _repository.GetByIdAsync(id);
            if (existingTeam == null)
                return false;

            existingTeam.Name = dto.Name;
            return await _repository.UpdateAsync(existingTeam);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var team = await _repository.GetByIdAsync(id);
            if (team == null)
                return false;

            var teamMembers = await _repository.GetTeamMembersAsync(id);
            if (teamMembers.Any())
                throw new InvalidOperationException("Cannot delete team with members.");

            var responsibilities = await _repository.GetResponsibilitiesByTeamAsync(id);
            if (responsibilities.Any())
                throw new InvalidOperationException("Cannot delete team assigned to a product.");

            return await _repository.DeleteAsync(id);
        }
    }
}


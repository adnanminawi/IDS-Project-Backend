using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;

namespace IDS.Services
{
    public class TeamMemberService : ITeamMemberService
    {

        private readonly ITeamMemberRepository _repository;

        public TeamMemberService (ITeamMemberRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TeamMember>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<TeamMember?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> CreateAsync(CreateTeamMemberDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Team Member name is required.");

            var teammember = new TeamMember
            {
                Name = dto.Name,
                Job = dto.Job,
                Department = dto.Department,
                Email = dto.Email,
                Status = dto.Status,
                Team_id = dto.Team_id,
                RoleInTeam = dto.RoleInTeam
            };
            return await _repository.CreateAsync(teammember);
        }
        public async Task<bool> UpdateAsync(int id, CreateTeamMemberDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Team Member name is required.");
            var existingMember = await _repository.GetByIdAsync(id);
            if (existingMember == null)
                return false;

            existingMember.Name = dto.Name;
            existingMember.Job = dto.Job;
            existingMember.Department = dto.Department;
            existingMember.Email = dto.Email;
            existingMember.Status = dto.Status;
            existingMember.Team_id = dto.Team_id;
            existingMember.RoleInTeam = dto.RoleInTeam;

            return await _repository.UpdateAsync(existingMember);

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);
            if (member == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}

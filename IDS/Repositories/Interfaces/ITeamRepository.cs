using IDS.Models.Entities;

namespace IDS.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task<int> CreateAsync(Team team);
        Task<bool> UpdateAsync(Team team);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TeamMember>> GetTeamMembersAsync(int teamId);
        Task<IEnumerable<Responsibility>> GetResponsibilitiesByTeamAsync(int teamId);
    }
}

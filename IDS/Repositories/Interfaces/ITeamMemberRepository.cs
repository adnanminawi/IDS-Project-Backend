using IDS.Models.Entities;

namespace IDS.Repositories.Interfaces
{
    public interface ITeamMemberRepository
    {
       public Task<IEnumerable<TeamMember>> GetAllAsync();
        public Task<TeamMember?> GetByIdAsync(int id);
        public Task<int> CreateAsync(TeamMember teamMember);
        public Task<bool> UpdateAsync(TeamMember teamMember);
        Task<bool> DeleteAsync(int id);
    }
}

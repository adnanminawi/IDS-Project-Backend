using IDS.Models.Dtos;
using IDS.Models.Entities;

namespace IDS.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMember>> GetAllAsync();
        Task<TeamMember?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTeamMemberDto dto);
        Task<bool> UpdateAsync(int id, CreateTeamMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

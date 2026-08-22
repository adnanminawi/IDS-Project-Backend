using IDS.Models.Dtos;
using IDS.Models.Entities;

namespace IDS.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTeamDto dto);
        Task<bool> UpdateAsync(int id, CreateTeamDto dto);
    }
}

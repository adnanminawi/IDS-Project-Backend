using IDS.Models.Dtos;
using IDS.Models.Entities;

namespace IDS.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserDto dto);
        
    }
}

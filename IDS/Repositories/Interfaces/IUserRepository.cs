using IDS.Models.Entities;

namespace IDS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
    }
}

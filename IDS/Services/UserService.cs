using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;
using BCrypt.Net;
namespace IDS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> CreateAsync (CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ArgumentException("Username is required.");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Password = hashedPassword,
                Role = dto.Role,
                IsActive = dto.IsActive
            };
            return await _repository.CreateAsync(user);

        }
    }
}

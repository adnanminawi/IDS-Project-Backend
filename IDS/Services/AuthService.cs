using IDS.Helpers;
using IDS.Models.Dtos;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;
namespace IDS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto) {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordValid)
                return null;

            var token = _tokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }

    }
}

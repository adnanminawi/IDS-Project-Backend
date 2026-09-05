using IDS.Helpers;
using IDS.Models.Dtos;
using IDS.Repositories;
using IDS.Repositories.Interfaces;
using IDS.Services.Interfaces;
namespace IDS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamMemberRepository _teamRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository, ITeamMemberRepository teamRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
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

            string? position = null;
            int? teamId = null;
            if (user.TeamMember_id.HasValue)
            {
                var teamMember = await _teamRepository.GetByIdAsync(user.TeamMember_id.Value);
                if (teamMember != null)
                {
                    position = teamMember.Position;
                    teamId = teamMember.Team_id;
                }
            }

            var token = _tokenGenerator.GenerateToken(user, position, teamId);

            return new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                Position = position,
                TeamId = teamId,
                Role = user.Role
            };
        }

    }
}

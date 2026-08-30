using Dapper;
using IDS.Data;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;

namespace IDS.Repositories
{
    public class TeamRepository : ITeamRepository
    {

        private readonly IDbConnectionFactory _factory;

        public TeamRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Team>("SELECT * FROM Teams");
        }
        public async Task<Team?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Team>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<TeamMember?>> GetTeamMembersAsync(int teamId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<TeamMember>("SELECT * FROM TeamMembers WHERE Team_id = @TeamId", new { TeamId = teamId });
        }
        public async Task<IEnumerable<Responsibility?>> GetResponsibilitiesByTeamAsync(int teamId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Responsibility>("SELECT * FROM Responsibilities WHERE Team_id = @TeamId", new { TeamId = teamId});
        }

        public async Task<int> CreateAsync(Team team)       
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Teams (Name)
                        VALUES (@Name); 
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.ExecuteScalarAsync<int>(sql, team);
        }
        public async Task<bool> UpdateAsync(Team team)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE Teams SET Name =@Name WHERE Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, team);
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"DELETE FROM Teams WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}

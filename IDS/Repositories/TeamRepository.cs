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
            var sql = @"UPDATE Teams SET Name =@Name";

            var rowsAffected = await connection.ExecuteAsync(sql, team);
            return rowsAffected > 0;
        }
    }
}

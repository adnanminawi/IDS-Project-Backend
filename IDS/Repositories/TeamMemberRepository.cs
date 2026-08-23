using Dapper;
using IDS.Data;
using IDS.Models.Dtos;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;

namespace IDS.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {

        private readonly IDbConnectionFactory _factory;

        public TeamMemberRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<TeamMember>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<TeamMember>("SELECT * FROM TeamMembers");
        }
        public async Task<TeamMember?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TeamMember>("SELECT * FROM TeamMembers WHERE Id = @Id", new { Id = id });
        }
        public async Task<int> CreateAsync (TeamMember teamMember)
        {
            using var connection = _factory.CreateConnection();

            var sql = @"INSERT INTO TeamMembers (Name, Job, Department, Email, Status, Team_id, RoleInTeam)
                        VALUES (@Name, @Job, @Department, @Email, @Status, @Team_id, @RoleInTeam);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.QuerySingleAsync<int>(sql, teamMember);
        }
        public async Task<bool> UpdateAsync(TeamMember teamMember)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE TeamMembers SET Name =@Name, Job=@Job, Department= @Department, Email =@Email, Status= @Status, Team_id= @Team_id, RoleInTeam=@RoleInTeam
                       WHERE Id=@Id";
            var rowsAffected = await connection.ExecuteAsync(sql,teamMember);
            return rowsAffected >0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            var sql = "DELETE FROM TeamMembers WHERE Id=@Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }


    }
}


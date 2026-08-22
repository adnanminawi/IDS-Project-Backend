using Dapper;
using IDS.Data;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;

namespace IDS.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }
        public async Task<int> CreateAsync(User user)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Users (Username, Password, Role, IsActive)
                VALUES (@Username, @Password, @Role, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, user);
        }
        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE Users SET Username =@Username, Password= @Password, Role=@Role, IsActive=@IsActive
                       WHERE Id=@Id";
            var rowsAffected = await connection.ExecuteAsync(sql, user);
            return rowsAffected > 0;
        
        }
    }
}
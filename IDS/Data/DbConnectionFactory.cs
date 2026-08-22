using System.Data;
using Microsoft.Data.SqlClient;

namespace IDS.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
using Dapper;
using IDS.Data;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;


namespace IDS.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnectionFactory _factory;
        
        public ClientRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Client>("SELECT * FROM Clients");
        }
        public async Task<Client?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WHERE Id= @id", new {Id = id});
        }
        public async Task<IEnumerable<Deployment>> GetDeploymentsByClientAsync(int clientId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Deployment>(
                "SELECT * FROM Deployments WHERE Client_id = @ClientId",
                new { ClientId = clientId });
        }
        public async Task<int> CreateAsync(Client client)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Clients (Name, Country, Contact, Status, Notes)
                VALUES (@Name, @Country, @Contact, @Status, @Notes);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.QuerySingleAsync<int>(sql, client);
        }
        public async Task<bool> UpdateAsync(Client client)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE Clients SET Name = @Name, Country = @Country, Contact = @Contact, Status = @Status, Notes = @Notes
                WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, client);
            return affectedRows > 0;
        }
    }
}

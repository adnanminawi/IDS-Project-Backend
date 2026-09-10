using Dapper;
using IDS.Data;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;
using System.Net.NetworkInformation;
using Environment = IDS.Models.Entities.Environment;

namespace IDS.Repositories
{
    public class DeploymentRepository : IDeploymentRepository
    {
        private readonly IDbConnectionFactory _factory;

        public DeploymentRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Deployment>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Deployment>("SELECT * FROM Deployments");
        }

        public async Task<Deployment?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Deployment>(
                "SELECT * FROM Deployments WHERE Id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<Environment>> GetEnvironmentsByDeploymentIdAsync(int deploymentId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Environment>(
            "SELECT * FROM Environments WHERE Deployment_id = @DeploymentId", new { DeploymentId = deploymentId });
        }
        public async Task<IEnumerable<Module>> GetModulesByDeploymentIdAsync(int deploymentId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Module>(
            "SELECT m.* FROM Modules m INNER JOIN DeploymentModules dm ON m.id=dm.Modules_id WHERE dm.Deployment_id = @DeploymentId", new { DeploymentId = deploymentId });
        }
        public async Task<IEnumerable<Deployment>> GetDeploymentsByTeamAsync(int teamId)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"SELECT d.* FROM Deployments d
                       INNER JOIN Responsibilities r ON d.Product_id = r.Product_id
                       WHERE r.Team_id = @TeamId";
            return await connection.QueryAsync<Deployment>(sql, new { TeamId = teamId });
        }

        public async Task<int> CreateAsync(Deployment deployment)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Deployments (Client_id, Product_id, Version, GoLiveDate, Status, SupportTier, ClientNotes)
                VALUES (@Client_id, @Product_id, @Version, @GoLiveDate, @Status, @SupportTier, @ClientNotes);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, deployment);
        }
        public async Task<bool> UpdateAsync(Deployment deployment)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE Deployments SET
                    Client_id = @Client_id,
                    Product_id = @Product_id,
                    Version = @Version,
                    GoLiveDate = @GoLiveDate,
                    Status = @Status,
                    SupportTier = @SupportTier,
                    ClientNotes = @ClientNotes
                WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, deployment);
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            var sql = "DELETE FROM Deployments WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
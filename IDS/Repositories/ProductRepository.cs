using Dapper;
using IDS.Data;
using IDS.Models.Entities;
using IDS.Repositories.Interfaces;

namespace IDS.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _factory;

        public ProductRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
            "SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<Module>> GetModulesByProductAsync(int productId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Module>(
            "SELECT * FROM Modules WHERE Product_id = @ProductId", new { ProductId = productId });   
        }
        public async Task<IEnumerable<Responsibility>> GetResponsibilitiesByProductAsync(int productId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Responsibility>(
            "SELECT * FROM Responsibilities WHERE Product_id = @ProductId", new { ProductId = productId });
        }
        public async Task<IEnumerable<Documentation>> GetDocumentationByProductAsync(int productId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Documentation>(
            "SELECT * FROM Documentation WHERE Product_id = @ProductId", new { ProductId = productId });
        }
        public async Task<IEnumerable<Repository>> GetRepositoriesByProductAsync(int productId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Repository>(
                "SELECT * FROM Repositories WHERE Product_id = @ProductId", new { ProductId = productId });
        }
        public async Task<IEnumerable<Deployment>> GetDeploymentsByProductAsync(int productId)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<Deployment>(
                "SELECT * FROM Deployments WHERE Product_id = @ProductId", new { ProductId = productId });
        }
        public async Task<int> CreateAsync(Product product)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Products (Name, Description, Purpose, Status, Version, Markets, Criticality, Technologies, Notes)
                VALUES (@Name, @Description, @Purpose, @Status, @Version, @Markets, @Criticality, @Technologies, @Notes);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            return await connection.ExecuteScalarAsync<int>(sql, product);
        }
        public async Task<int> CreateResponsibilityAsync(Responsibility responsibility)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Responsibilities (Product_id, Team_id, Description)
                VALUES (@Product_id, @Team_id, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.QuerySingleAsync<int>(sql, responsibility);
        }
        public async Task<int> CreateModuleAsync(Module module)
        {
            using var connection = _factory.CreateConnection();
            var sql = @"INSERT INTO Modules (Product_id, Name, Description, Status)
                VALUES (@Product_id, @Name, @Description, @Status);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.QuerySingleAsync<int>(sql, module);
        }
        public async Task<bool> UpdateAsync(Product product) 
        {
            using var connection = _factory.CreateConnection();
            var sql = @"UPDATE Products SET 
                Name = @Name, 
                Description = @Description, 
                Purpose = @Purpose, 
                Status = @Status, 
                Version = @Version, 
                Markets = @Markets, 
                Criticality = @Criticality, 
                Technologies = @Technologies, 
                Notes = @Notes
                WHERE Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, product);
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _factory.CreateConnection(); 
            var sql = "DELETE FROM Products WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;    
        }
    }
}
using Biruwaa.DataAccess.Data;
using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Biruwaa.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public ProductRepository(AppDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }

        public async Task<IEnumerable<Product>> Got()
        {
            return await _connection.QueryAsync<Product>("GetAllProducts", commandType: CommandType.StoredProcedure);
        }
    }
}

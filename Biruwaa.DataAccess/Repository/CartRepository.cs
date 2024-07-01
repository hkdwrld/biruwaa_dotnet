using Biruwaa.DataAccess.Data;
using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Biruwaa.DataAccess.Repository
{
    //Repository<Order>, IOrderRepository
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public CartRepository(AppDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Cart>> Got(string uid)
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@AuthUserId", uid);
            return await _connection.QueryAsync<Cart>("GetAllUserCart", parmeters, commandType: CommandType.StoredProcedure);
        }

        public void Update(Cart obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> Add(Cart cart, string uid)
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@AuthUserId", uid);
            parmeters.Add("@ProductId", cart.ProductId);
            parmeters.Add("@Count", cart.Count);
            await _connection.ExecuteAsync("AddToCart", parmeters, commandType: CommandType.StoredProcedure);
            return cart;
        }
    }
}

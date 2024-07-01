using Biruwaa.DataAccess.Data;
using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;

namespace Biruwaa.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private AppDbContext _db;
        public OrderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

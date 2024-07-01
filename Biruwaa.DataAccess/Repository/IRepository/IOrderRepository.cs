using Biruwaa.Models;

namespace Biruwaa.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Category obj);
        void Save();
    }
}

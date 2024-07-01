using Biruwaa.Models;

namespace Biruwaa.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

        void Save();

        Task<IEnumerable<Product>> Got();
    }
}

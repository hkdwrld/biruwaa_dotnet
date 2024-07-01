using Biruwaa.Models;

namespace Biruwaa.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Update(Cart obj);
        void Save();

        Task<IEnumerable<Cart>> Got(string uid);

        Task<Cart> Add(Cart cart, string uid);
    }
}

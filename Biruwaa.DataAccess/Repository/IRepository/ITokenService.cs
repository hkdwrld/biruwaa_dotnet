using Biruwaa.Models;

namespace Biruwaa.DataAccess.Repository.IRepository
{
    public interface ITokenService
    {
        string CreateToken(AuthUser appUser);
    }
}

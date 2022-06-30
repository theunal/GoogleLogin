using Core.Entities;
using Core.JWT;
using Core.Utilities;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        void Add(string name, string email, string password);
        AccessToken CreateAccessToken(User user);
        Response Login(string email, string password);
    }
}

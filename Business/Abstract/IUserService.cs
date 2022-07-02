using Core.Dtos;
using Core.Entities;
using Core.JWT;
using Core.Utilities;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        IResult Add(string name, string email, string password);
        AccessToken CreateAccessToken(User user);
        Response Login(string email, string password);
        AccessToken GoogleLogin(GoogleLoginDto dto);
    }
}

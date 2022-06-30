using Core.Entities;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        List<User> GetAll();
        void Add(User user);
        List<OperationClaim> GetClaims(User user);
        User GetByEmail(string email);
    }
}

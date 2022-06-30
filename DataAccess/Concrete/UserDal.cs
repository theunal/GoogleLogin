using Core.Entities;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserDal : IUserDal
    {
        public void Add(User user)
        {
            using var context = new DataContext();
            var addedEntity = context.Entry(user);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public List<User> GetAll()
        {
            using var context = new DataContext();
            return context.Users.ToList();
        }

        public User GetByEmail(string email)
        {
            using var context = new DataContext();
            return context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DataContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };

                return result.ToList();
            }
        }
    }
}

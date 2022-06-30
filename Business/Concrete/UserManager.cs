using Business.Abstract;
using Core.Entities;
using Core.Hashing;
using Core.JWT;
using Core.Utilities;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        private readonly ITokenHelper tokenHelper;
        public UserManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            this.userDal = userDal;
            this.tokenHelper = tokenHelper;
        }

        public void Add(string name, string email, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            User user = new User
            {
                Email = email,
                Name = name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            userDal.Add(user);
        }

        public AccessToken CreateAccessToken(User user)
        {   // giriş tokeni 
            var claims = userDal.GetClaims(user);
            return tokenHelper.CreateToken(user, claims);
        }

        public List<User> GetAll()
        {
            return userDal.GetAll();
        }

        public Response Login(string email, string password)
        {
            var userToCheck = userDal.GetByEmail(email);
            if (userToCheck is null) return new Response { User = null, Message = "Kullanıcı bulunamadı." };

            return HashingHelper.VerifyPasswordHash(password, userToCheck.PasswordHash, userToCheck.PasswordSalt) ?
                       new Response { User = userToCheck, Message = "Giriş Başarılı." } : // giriş başarılı
                       new Response { User = null, Message = "Şifre Yanlış." }; // şifre yanlış
        }
    }
}

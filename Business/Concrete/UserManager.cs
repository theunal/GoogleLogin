using Business.Abstract;
using Core.Dtos;
using Core.Entities;
using Core.Hashing;
using Core.JWT;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        public IConfiguration Configuration { get; }


        private readonly IUserDal userDal;
        private readonly ITokenHelper tokenHelper;
        private readonly TokenOptions tokenOptions;
        public UserManager(IUserDal userDal, ITokenHelper tokenHelper, IConfiguration configuration)
        {
            this.userDal = userDal; this.tokenHelper = tokenHelper;

            Configuration = configuration;
            this.tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public IResult Add(string name, string email, string password)
        {
            var userToCheck = userDal.GetByEmail(email);
            if (userToCheck is not null) return new ErrorResult("Email adresi daha önce kayıt edilmiş.");

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
            return new SuccessResult("Kullanıcı oluşturuldu.");
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

        public AccessToken GoogleLogin(GoogleLoginDto dto)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(dto.IdToken);
            var email = jwtSecurityToken.Claims.First(x => x.Type == "email").Value;
            var key = tokenOptions.SecurityKey; //app.json dan gelen key. bunun ile emaili birleştirip şifre yaptım

            var userToCheck = userDal.GetByEmail(dto.Email);
            if (userToCheck is null)
            {
                Add(dto.Name, dto.Email, key+email);
            }
            var user = Login(dto.Email, key + email).User;
            return CreateAccessToken(user);
        }
    }
}

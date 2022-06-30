using Microsoft.IdentityModel.Tokens;

namespace Core.Encryption
{
    public class SigningCredentialsHelper
    {

        // asp.net in kendi kullanacagı algoritmayı burda yazıyoruz
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

    }
}
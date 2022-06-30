using Core.Entities;

namespace Core.JWT
{
    // token burda olusturuluyor
    public interface ITokenHelper
    {
        // user ve onun operation claimleri için token oluşturuluyor
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
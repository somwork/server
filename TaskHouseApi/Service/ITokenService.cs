using System.Collections.Generic;
using System.Security.Claims;

namespace TaskHouseApi.Service
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken);
    }
}
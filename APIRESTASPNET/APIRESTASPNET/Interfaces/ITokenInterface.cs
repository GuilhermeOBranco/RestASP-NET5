using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace APIRESTASPNET.Interface
{
    public interface ITokenInterface
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string Token);
    }
}
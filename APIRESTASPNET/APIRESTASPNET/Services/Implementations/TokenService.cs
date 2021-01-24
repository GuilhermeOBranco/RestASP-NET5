using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APIRESTASPNET.Configurations;
using APIRESTASPNET.Interface;
using Microsoft.IdentityModel.Tokens;

namespace APIRESTASPNET.Services.Implementations
{
    public class TokenService : ITokenInterface
    {
        private readonly TokenConfiguration _configuration;
        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secrete));
            var signInCredentials = new SigningCredentials(secreteKey,SecurityAlgorithms.HmacSha256);

            var TokenOptions = new JwtSecurityToken(issuer: _configuration.Issuer,
                                                    audience: _configuration.Audience,
                                                    claims: claims,
                                                    null,
                                                    expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                                                    signingCredentials: signInCredentials);
            
            return new JwtSecurityTokenHandler().WriteToken(TokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using(var Rng = RandomNumberGenerator.Create())
            {
                Rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string Token)
        {
            var TokenValidationParameters = new TokenValidationParameters{
                ValidateAudience= false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secrete)),
                ValidateLifetime = false
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;


            var principal = TokenHandler.ValidateToken(Token, TokenValidationParameters, out securityToken);

            var JwtSecurityToken = securityToken as JwtSecurityToken;
            if(JwtSecurityToken == null || !JwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
    }
}
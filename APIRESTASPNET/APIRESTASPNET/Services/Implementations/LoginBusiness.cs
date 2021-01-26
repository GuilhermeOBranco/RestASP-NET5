using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using APIRESTASPNET.Configurations;
using APIRESTASPNET.Interface;
using APIRESTASPNET.Models;

namespace APIRESTASPNET.Services.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfiguration _configuration;
        private readonly UserService _service;
        private readonly ITokenInterface _TokenService;

        public LoginBusiness(TokenConfiguration configuration, UserService service, ITokenInterface token)
        {
            _configuration = configuration;
            _service = service;
            _TokenService = token;
        }
        public async Task<TokenVO> ValidateCredentials(UserVO userCredentials)
        {
            User user = await _service.ValidateCredentials(userCredentials);
            if(user == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var AccessToken = _TokenService.GenerateAccessToken(claims);
            var refreshToken = _TokenService.GenerateRefreshToken();

            user.RefreshToke = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                AccessToken,
                refreshToken    

            );
        }


    }
}
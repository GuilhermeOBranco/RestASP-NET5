using System;

namespace APIRESTASPNET.Models
{
    public class TokenVO
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public TokenVO(bool auth, string crea, string exp, string access, string refresh)
        {
            Authenticated = auth;
            Created = crea;
            Expiration = exp;
            AccessToken = access;
            RefreshToken = refresh;
        }
    }
}
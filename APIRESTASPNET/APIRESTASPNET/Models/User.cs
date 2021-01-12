using System;

namespace APIRESTASPNET.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string RefreshToke { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
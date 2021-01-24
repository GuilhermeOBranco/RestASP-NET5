using System;

namespace APIRESTASPNET.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secrete { get; set; }
        public int Minutes { get; set; }
        public int DaysToExpire { get; set; }

    }
}
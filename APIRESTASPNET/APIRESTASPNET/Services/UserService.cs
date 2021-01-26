using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APIRESTASPNET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIRESTASPNET.Services
{
    public class UserService
    {

        private readonly string _connectionString;

        public UserService()
        {
            _connectionString = @"Server=.\SQLExpress;Database=DB_API_ASP;Trusted_Connection=Yes;";
        }

        public async Task<User> ValidateCredentials(UserVO user)
        {
            User usuario = new User();

            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            string query = @"SELECT 
                                USER_ID, 
                                USERNAME, 
                                USRPASSWORD, 
                                USER_FULL_NAME,
                                REFRESH_TOKEN,
                                REFRESH_TOKEN_EXPIRY_TIME  
                            FROM USERS_AUTH 
                                WHERE USERNAME = @USERNAME
                                AND USRPASSWORD = @PASS";

            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@USERNAME", user.UserName);
                cmd.Parameters.AddWithValue("@PASS", user.Password);
                
                using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        usuario.Id                     = (int)      reader[0];
                        usuario.Username               = (string)   reader[1];
                        usuario.Password               = (string)   reader[2];
                        usuario.FullName               = (string)   reader[3];
                        usuario.RefreshToke            = (string)   reader[4];
                        usuario.RefreshTokenExpiryTime = (DateTime) reader[5];
                    }
                }
                return usuario;

            }
        }

        public async Task<User> UpdateToken(User user)
        {
            string query = "";
            throw new NotImplementedException();
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
           Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
           Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
           return BitConverter.ToString(hashedBytes);
        }
    }
}

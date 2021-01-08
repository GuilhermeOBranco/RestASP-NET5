using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIRESTASPNET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIRESTASPNET.Services.Implementations
{
    public class PersonServiceImplementation
    {

        private readonly string _connectionString;

        public PersonServiceImplementation(IConfiguration config)
        {
            _connectionString = @"Server=.\SQLExpress;Database=DB_API_ASP;Trusted_Connection=Yes;";
        }
        public async Task<bool> Create(Person person)
        {
            string insert = $@"INSERT INTO PERSON 
			                    (PERSON_FIRST_NAME, PERSON_LAST_NAME, PERSON_ADDRESS, GENDER) 
                            VALUES		
			                    (@fstname, @lstname, @addr, @gender);";
            try
            {
                using(SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand(insert,con);

                    cmd.Parameters.AddWithValue("@fstname", person.FirstName);
                    cmd.Parameters.AddWithValue("@lstname", person.LastName);
                    cmd.Parameters.AddWithValue("@addr", person.Address);
                    cmd.Parameters.AddWithValue("@gender", person.Gender);

                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }

            }catch(SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Delete(long id)
        {

        }

        public async Task<List<Person>> FindById(long id)
        {
            List<Person> pessoas = new List<Person>();
            string query = "SELECT PERSON_ID, PERSON_FIRST_NAME, PERSON_LAST_NAME, PERSON_ADDRESS, GENDER FROM PERSON WHERE PERSON_ID = @ID";
            
            try
            {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@ID",id);
                using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        pessoas.Add(new Person(
                            (int)reader[0],
                            (string)reader[1],
                            (string)reader[2],
                            (string)reader[3],
                            (string)reader[4]
                        ));
                    }
                    return pessoas;
                }
            }
            }
            catch(SqlException ex)
            {
                string error = ex.ToString();
                throw new Exception(error);
            }
        }

        public async Task<List<Person>> FindAll()
        {
            string query = "SELECT PERSON_ID, PERSON_FIRST_NAME, PERSON_LAST_NAME, PERSON_ADDRESS, GENDER FROM PERSON";
            List<Person> pessoas = new List<Person>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand cmd = new SqlCommand(query, connection);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        pessoas.Add(new Person
                        (
                            (int)reader[0],
                            (string)reader[1],
                            (string)reader[2],
                            (string)reader[3],
                            (string)reader[4]
                        ));
                    }
                    return pessoas;
                }
            }
        }

        public async Task<bool> Update(Person person)
        {
            string update = $@"UPDATE PERSON
                                SET PERSON_FIRST_NAME = @FirstName, 
                                    PERSON_LAST_NAME  = @LastName, 
                                    PERSON_ADDRESS    = @Address, 
                                    GENDER            = @Gender   
                                WHERE PERSON_ID = @Id;
                            ";
            try
            {
                using(SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand(update,con);
                    
                    cmd.Parameters.AddWithValue("@FirstName",person.FirstName);
                    cmd.Parameters.AddWithValue("@LastName",person.LastName);
                    cmd.Parameters.AddWithValue("@Address",person.Address);
                    cmd.Parameters.AddWithValue("@Gender", person.Gender);
                    cmd.Parameters.AddWithValue("@Id", person.Id);

                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
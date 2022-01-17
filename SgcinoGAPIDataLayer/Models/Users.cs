using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Models
{
    public class Users
    {
        private readonly string dbConnectionString;
        private int result = 0;
        public Users(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }

        public bool Register()
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"INSERT INTO [dbo].Users
                                                           ([Name]
                                                           ,[Surname]
                                                           ,[Created]
                                                           ,[Username]
                                                           )
                                                     VALUES
                                                           (@Name
                                                           ,@Surname
                                                           ,@Created
                                                           ,@Username
                                                           )", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                cmd.Parameters.AddWithValue("@Username", Username);
                result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0;
        }

        public bool Update()
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"UPDATE [dbo].Users SET NAME=@Name, Surname=@Surname", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0;
        }

        public void Get()
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"INSERT INTO [dbo].Users
                                                           ([Name]
                                                           ,[Surname]
                                                           ,[Created]
                                                           ,[Username]
                                                           )
                                                     VALUES
                                                           (@Name
                                                           ,@Surname
                                                           ,@Created
                                                           ,@Username
                                                           )", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete()
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"INSERT INTO [dbo].Users
                                                           ([Name]
                                                           ,[Surname]
                                                           ,[Created]
                                                           ,[Username]
                                                           )
                                                     VALUES
                                                           (@Name
                                                           ,@Surname
                                                           ,@Created
                                                           ,@Username
                                                           )", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgcinoGAPIDataModels.Models;

namespace SgcinoGAPIDataLayer.Models
{
    public class Orders
    {
        private string dbConnectionString;
      
        public int Id { get; set; }
        public List<int> Products { get; set; }
        public DateTime Created { get; set; }
        public int OrderNum { get; set; }
        public string UserId { get; set; }

        public Orders(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public string AddOrder()
        {
            var orderId = "";
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"INSERT INTO [dbo].[Order]([Created],[UserId]) OUTPUT Inserted.Id VALUES (@Created,@UserId)", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Created", Created);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                orderId = cmd.ExecuteScalar().ToString();
                connection.Close();
            }
            return orderId;
        }
    }
}

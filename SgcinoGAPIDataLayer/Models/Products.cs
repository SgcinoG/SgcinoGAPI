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
    public class Products 
    {
        private string dbConnectionString;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public int OrderId { get; set; }

        public List<int> OrderProducts { get; set; }

        public Products(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public bool Add()
        {
            var result = 0;
            using (var connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();

                foreach (var prod in OrderProducts)
                {
                    var cmd = new SqlCommand(@"INSERT INTO [dbo].[Orders]([ProductId],[OrderNum]) VALUES (@ProductId,@OrderNum)", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductId", prod);
                    cmd.Parameters.AddWithValue("@OrderNum", OrderId);
                    result += cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
            return result > 0;
        }
    }
}

using SgcinoGAPIDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Factories
{
    public class OrdersFactory
    {
        private readonly string dbConnectionString;
        public OrdersFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public Orders Create(Action<Orders> initializer)
        {
            var newOrder = new Orders(this.dbConnectionString);
            initializer(newOrder);
            return newOrder;
        }

        public string GetOrder(int orderNum)
        {
            return "";
        }

        public double CalculateOrderTotal(int orderNum)
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"SELECT sum(P.Price) Total
                                                                FROM[SgcinoGAPIDB].[dbo].[Order] O
                                                                JOIN dbo.Orders OS ON OS.OrderNum = O.ID
                                                                JOIN Products P ON OS.ProductId = P.Id
                                                              
                                                                WHERE O.Id = @OrderNum", connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderNum", orderNum);
                var result = cmd.ExecuteScalar().ToString();
                connection.Close();
                return double.Parse(result);
                
            }
            //SELECT O.Id, Sum(P.Price) Total, U.Name
            //FROM[SgcinoGAPIDB].[dbo].[Order] O
            //JOIN dbo.Orders OS ON OS.OrderNum = O.ID
            //JOIN Products P ON OS.ProductId = P.Id
            //JOIN Users U ON O.UserId = U.UserId
            //GROUP By O.Id, U.Name
        }

        public string GetUserLatestOrder(string userId)
        {
            using (var connection = new SqlConnection(this.dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"SELECT TOP(1) Id
                                            FROM dbo.[Order]
                                            Where UserId = @UserId
                                            Order By Created desc", connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.CommandType = System.Data.CommandType.Text;
                return cmd.ExecuteScalar().ToString();
            }
        }
    }
}

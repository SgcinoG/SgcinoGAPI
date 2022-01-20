using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Models
{
    public class Transaction
    {
        public string dbConnectionString { get; set; }
        public Transaction(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public long MeterNumber { get; set; }
        public string CduId { get; set; }
        public bool Add()
        {
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("INSERT INTO [dbo].Transactions(Amount,Status,Created,MeterNumber,CduId) VALUES(@Amount,@Status,@Created,@MeterNumber,@CduId)", connection);
                cmd.Parameters.Add(new SqlParameter("@Status", Status));
                cmd.Parameters.Add(new SqlParameter("@Amount", Amount));
                cmd.Parameters.Add(new SqlParameter("@Created", Created));
                cmd.Parameters.Add(new SqlParameter("@CduId", CduId));
                cmd.Parameters.Add(new SqlParameter("@MeterNumber", MeterNumber));
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandType = System.Data.CommandType.Text;
                var result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        public bool Update()
        {
            var result = 0;
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("UPDATE [dbo].Transactions SET Status=@StatusId WHERE Id=@Id", connection);
                cmd.Parameters.Add(new SqlParameter("@StatusId", Status));
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                cmd.CommandType = System.Data.CommandType.Text;
                result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0;
        }
    }
}

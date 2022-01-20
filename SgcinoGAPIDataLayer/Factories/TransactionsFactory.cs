using SgcinoGAPIDataLayer.Models;
using SgcinoGAPIDataModels.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SgcinoGAPIDataLayer.Factories
{
    public class TransactionsFactory
    {
        public string dbConnectionString { get; set; }
        public TransactionsFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public Transaction Create(Action<Transaction> initalizer)
        {
            var newTransaction = new Transaction(this.dbConnectionString);
            initalizer(newTransaction);
            return newTransaction;
        }

        public List<TransactionCtrl> GetTransactionsByType(int transactionStatus)
        {
            var limit = 0;
            var transactions = new List<TransactionCtrl>();
            using (var connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT * FROM [dbo].Transactions WHERE Status=@StatusType ORDER BY Created ", connection);
                cmd.Parameters.Add(new SqlParameter("@StatusType", transactionStatus));
                cmd.CommandType = System.Data.CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read() && limit < 100)
                    {
                        var transaction = new TransactionCtrl();
                        transaction.Amount = double.Parse(reader["Amount"].ToString());
                        transaction.MeterNumber = long.Parse(reader["MeterNumber"].ToString());
                        transaction.Created = DateTime.Parse(reader["Created"].ToString());
                        transaction.CduId = reader["CduId"].ToString();
                        transaction.Status = int.Parse(reader["Status"].ToString());
                        transaction.Id = int.Parse(reader["Id"].ToString());
                        transactions.Add(transaction);
                        limit++;
                    }
                }
                connection.Close();
            }
            return transactions;
        }
    }
}

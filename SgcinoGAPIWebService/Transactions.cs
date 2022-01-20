using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;

namespace SgcinoGAPIWebService
{
    public class Transactions
    {
        private readonly OrdersBl ordersBl;
        private System.Timers.Timer AddTransactionsTimer;
        private System.Timers.Timer GetTransactionsTimer;
        public bool canAdd = true;
        public bool canGet = true;
        public Transactions(string dbConnectionString)
        {
            ordersBl = new OrdersBl(dbConnectionString);
        }

        public void Start()
        {
            AddTransactionsTimer = new System.Timers.Timer();//3seconds
            AddTransactionsTimer.Interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AddTransactions"]); //fetch from config file
            AddTransactionsTimer.Elapsed += AddTransactionsEvent;
            AddTransactionsTimer.AutoReset = true;
            AddTransactionsTimer.Enabled = true;

            GetTransactionsTimer = new System.Timers.Timer();//3seconds
            GetTransactionsTimer.Interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["GetTransactions"]);
            GetTransactionsTimer.Elapsed += ProcessTransactionsEvent;
            GetTransactionsTimer.AutoReset = true;
            GetTransactionsTimer.Enabled = true;
            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();
        }

        public void Stop()
        {
            AddTransactionsTimer.Stop();
            AddTransactionsTimer.Dispose();
            GetTransactionsTimer.Stop();
            GetTransactionsTimer.Dispose();
            canAdd = false;
            canGet = false;
        }

        private void AddTransactionsEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("New Transaction at {0}", e.SignalTime); //Add New Transaction
            AddTransaction().Wait();
        }

        private void ProcessTransactionsEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Updating transaction at {0}", e.SignalTime); //Add New Transaction
            ProcessTransactions();
            //ProcessTransactions_().Wait();
        }

        public async Task ProcessTransactions_()
        {
            try
            {
                if (canGet)
                {
                    canGet = false; //Lock
                    var transactions = ordersBl.GetTransactions((int)TransactionStatus.Pending);
                    Console.WriteLine("Processing transactions at {0}", DateTime.Now); //Add New Transaction
                    Thread.Sleep(10000);
                    foreach (var transaction in transactions)
                    {
                        //update as success
                        transaction.Status = (int)TransactionStatus.Success;
                        var response = ordersBl.UpdateTransaction(transaction);
                    }
                    Console.WriteLine("Processing complete at {0}", DateTime.Now); //Add New Transaction

                    //Check result? 
                }
                return;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                canGet = true;
            }
        }

        public void ProcessTransactions()
        {
            try
            {
                if (canGet)
                {
                    canGet = false; //Lock
                    var transactions = ordersBl.GetTransactions((int)TransactionStatus.Pending);
                    Console.WriteLine("Processing transactions at {0}", DateTime.Now); //Add New Transaction
                    Thread.Sleep(5000);
                    foreach (var transaction in transactions)
                    {
                        //update as success
                        transaction.Status = (int)TransactionStatus.Success;
                        var response = ordersBl.UpdateTransaction(transaction);
                    }
                    Console.WriteLine("Processing complete at {0}", DateTime.Now); //Add New Transaction

                    //Check result? 
                }
                return;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                canGet = true;
            }
        }

        public async Task AddTransaction()
        {
            try
            {
                if (canAdd)
                {
                    canAdd = false; //Lock
                    Random random = new Random();
                    int stringlen = random.Next(4, 10);
                    int randValue;
                    string str = "";
                    char letter;
                    for (int i = 0; i < 4; i++)
                    {

                        // Generating a random number.
                        randValue = random.Next(0, 26);

                        // Generating random character by converting
                        // the random number into character.
                        letter = Convert.ToChar(randValue + 65);

                        // Appending the letter to string.
                        str = str + letter;
                    }

                    
                    var amount = random.Next(1, 10000);
                    long meterNumber = random.Next(100000000, 999999999);
                    meterNumber += 10000000001;
                    var CduId = str+meterNumber.ToString().Substring(0, 2);
                    
                    var response = ordersBl.AddTransaction(new TransactionCtrl
                    {
                        Status = (int)TransactionStatus.Pending,
                        Amount = amount,
                        CduId = CduId,
                        Created = DateTime.Now,
                        MeterNumber = meterNumber
                    });
                    //Check result? 
                }
                return;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                canAdd = true;
            }
        }
    }
}

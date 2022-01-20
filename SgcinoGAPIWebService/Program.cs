using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SgcinoGAPIWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.SetInstanceName("TransactionService");
                x.SetDisplayName("Transaction Service");
                x.SetDescription("Transaction Service, Imports transactions.");
                x.Service<Transactions>(servconfig =>
                {
                    servconfig.ConstructUsing(() => new Transactions("Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"));
                    servconfig.WhenStarted(s => s.Start());
                    servconfig.WhenStopped(s => s.Stop());
                });
            });
            //init businesslayers etc.
            //Add transaction every three seconds
            //Process /Update as successfull - mimic work (5s)
        }
    }
}

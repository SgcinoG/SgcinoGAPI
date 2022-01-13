using SgcinoGAPIDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Factories
{
    public class UsersFactory
    {
        private readonly string dbConnectionString;

        public UsersFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public Users Create(Action<Users> initalizer)
        {
            var newUser = new Users(this.dbConnectionString);
            initalizer(newUser);
            return newUser;
        }
    }
}

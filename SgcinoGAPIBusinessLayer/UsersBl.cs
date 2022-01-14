using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgcinoGAPIDataLayer.Models;
using SgcinoGAPIDataLayer.Factories;
using SgcinoGAPIDataLayer;
using SgcinoGAPIDataModels.Models;

namespace SgcinoGAPIBusinessLayer
{
    /// <summary>
    /// Business Layer for users model
    /// </summary>
    public class UsersBl
    {
        private readonly IApiConnectionSettings apiConnectionSettings;
        private readonly UsersFactory usersFactory;
        public UsersBl(IApiConnectionSettings apiConnectionSettings)
        {
            this.apiConnectionSettings = apiConnectionSettings;
            usersFactory = new UsersFactory(apiConnectionSettings.GetConnectionString());
        }

        public UsersBl(string dbConnectionString)
        {
            usersFactory = new UsersFactory(dbConnectionString);
        }

        public UsersResponseCtrl RegisterUser(UsersCtrl newUser)
        {
            var userResponse = new UsersResponseCtrl { };
            try
            {
                var newUserInsert = usersFactory.Create(x =>
                {
                    x.Created = newUser.Created;
                    x.Name = newUser.Name;
                    x.Surname = newUser.Surname;
                    x.Username = newUser.Username;
                });

                if (!newUserInsert.Register())
                    throw new Exception("");
                userResponse.Status = ResponseStatus.Success;
                return userResponse;
            }
            catch (Exception E)
            {
                userResponse.Status = ResponseStatus.Error;
                userResponse.Error.ExceptionMessage = E.Message;
                return userResponse;
            }
        }
    }
}

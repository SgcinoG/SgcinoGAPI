using SgcinoGAPIDataLayer.Models;
using System;
using SgcinoGAPIDataLayer.SgcinoData;

namespace SgcinoGAPIDataLayer.Factories
{
    public interface IDbstringFactory
    {
        EntityDbString Create(Action<EntityDbString> initalizer);
        string GetConnectionString();
    }
    public class EntityDbStringFactory : IDbstringFactory
    {
        private readonly string dbConnectionString;
        private readonly ISgcinoDataSettings sgcinoDataSettings;

        /// <summary>
        /// Constructor accepts an already initialised dataSetting that contains the relevant db connection string
        /// </summary>
        /// <param name="sgcinoDataSettings"></param>
        public EntityDbStringFactory(ISgcinoDataSettings sgcinoDataSettings)
        {
            dbConnectionString = sgcinoDataSettings.GetEntityDBConnectionString();
            this.sgcinoDataSettings = sgcinoDataSettings;
        }

        public EntityDbString Create(Action<EntityDbString> initalizer)
        {

            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {

            return dbConnectionString;
        }
    }
}

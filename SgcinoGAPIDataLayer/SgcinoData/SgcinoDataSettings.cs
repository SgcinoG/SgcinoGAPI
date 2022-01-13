using System.Configuration;

namespace SgcinoGAPIDataLayer.SgcinoData
{
    public interface ISgcinoDataSettings
    {
        string GetEntityDBConnectionString();
    }
    public class SgcinoDataSettings : ISgcinoDataSettings
    {
        /// <summary>
        /// Gets connection string, set in web config
        /// </summary>
        /// <returns></returns>
        public string GetEntityDBConnectionString()
        {
            string conn = ConfigurationManager.ConnectionStrings["SgcinoDB"].ConnectionString;
            return conn;
        }
    }
}

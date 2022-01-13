using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer
{
    public interface IApiConnectionSettings
    {
        string GetConnectionString();
    }
    public class SgcinoAPIDataLayer : IApiConnectionSettings
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SgcinoDB"].ConnectionString;
        }
    }
}

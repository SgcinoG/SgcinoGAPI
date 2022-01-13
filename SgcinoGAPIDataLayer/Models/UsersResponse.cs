using ContourConstants;
using SgcinoGAPIDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Models
{
    public class UsersResponse : GenericResponse
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
    }
}

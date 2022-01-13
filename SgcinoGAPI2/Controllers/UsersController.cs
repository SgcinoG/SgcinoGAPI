using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataModels.Models;
using System.Web.Http;

namespace SgcinoGAPI2.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : ApiController
    {
        private readonly UsersBl usersBl;
        //pass in BL that was injected
        public UsersController(UsersBl usersBl)
        {
            this.usersBl = usersBl;
        }

        [Authorize]
        [HttpPost, Route("Register")]
        public UsersResponseCtrl Register([FromBody] UsersCtrl newUser)
        {
            return usersBl.RegisterUser(newUser); ;
        }
    }
}

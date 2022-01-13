using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataLayer;
using SgcinoGAPIDataLayer.Models;
using ContourConstants;

namespace SgcinoGAPI.Controllers
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
        public bool Register([FromBody] Users newUser)
        {
            usersBl.RegisterUser(newUser);
            return true;
        }
    }
}
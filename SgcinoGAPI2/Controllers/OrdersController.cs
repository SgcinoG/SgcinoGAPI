using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataLayer.Models;
using SgcinoGAPIDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SgcinoGAPI2.Controllers
{
    [RoutePrefix("Orders")]
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly OrdersBl ordersBl;
        public OrdersController(OrdersBl ordersBl)
        {
            this.ordersBl = ordersBl;
        }

        [HttpPost, Route("AddOrder")]
        public HttpResponseMessage AddOrder([FromBody] OrdersCtrl newOrder)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ordersBl.AddOrder(newOrder));
        }

        [HttpPost, Route("AddProducts")]
        public HttpResponseMessage AddProducts([FromBody] OrdersCtrl newOrder)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ordersBl.AddProducts(newOrder));
        }

        [HttpPost, Route("GetLatestOrder")]
        public HttpResponseMessage GetUserLatestOrder([FromBody]string userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ordersBl.GetUserLatestOrder(userId));
        }

        [HttpPost, Route("GetTotal")]
        public HttpResponseMessage GetTotal([FromBody] int orderNum)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ordersBl.GetTotal(orderNum));
        }
       
    }
}
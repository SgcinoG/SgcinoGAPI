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
        public OrdersResponseCtrl AddOrder([FromBody] OrdersCtrl newOrder)
        {
            return ordersBl.AddOrder(newOrder);
        }

        [HttpPost, Route("AddProducts")]
        public ProductResponseCtrl AddProducts([FromBody] OrdersCtrl newOrder)
        {
            return ordersBl.AddProducts(newOrder);
        }

        [HttpPost, Route("GetLatestOrder")]
        public string GetUserLatestOrder([FromBody]string userId)
        {
            return ordersBl.GetUserLatestOrder(userId);
        }

        [HttpPost, Route("GetTotal")]
        public double GetTotal([FromBody] int orderNum)
        {
            return ordersBl.GetTotal(orderNum);
        }
       
    }
}
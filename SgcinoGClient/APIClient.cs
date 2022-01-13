using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contour.BaseClient;
using Newtonsoft.Json;
using SgcinoGAPIDataLayer.Models;
using SgcinoGAPIDataModels.Models;

namespace SgcinoGClient
{
    public class APIClient: BaseClient
    {
        public APIClient(string username, string password, string url) : base(username, password, url)
        {

        }

        public OrdersResponseCtrl AddOrder(OrdersCtrl order)
        {
            var response = PerformPostOperation("Orders/AddOrder", order);

            return JsonConvert.DeserializeObject<OrdersResponseCtrl>(response);
        }

        public ProductResponseCtrl AddProducts(OrdersCtrl order)
        {
            var response = PerformPostOperation("Orders/AddProducts", order);

            return JsonConvert.DeserializeObject<ProductResponseCtrl>(response);
        }

        public string GetTotal(int orderNum)
        {
            return PerformPostOperation("Orders/GetTotal", orderNum);
            //return JsonConvert.DeserializeObject<bool>(result);
        }

        public string GetUserLatestOrder(string userId)
        {
            return PerformPostOperation("Orders/GetLatestOrder", userId);
        }

        public UsersResponseCtrl RegisterUser(UsersCtrl user)
        {
            var method = "Users/Register";
            var result = PerformPostOperation(method, user);
            return JsonConvert.DeserializeObject<UsersResponseCtrl>(result);
        }
    }
}

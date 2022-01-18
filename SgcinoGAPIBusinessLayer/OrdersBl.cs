using SgcinoGAPIDataLayer;
using SgcinoGAPIDataLayer.Factories;
using SgcinoGAPIDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgcinoGAPIDataModels.Models;

namespace SgcinoGAPIBusinessLayer
{
    public class OrdersBl
    {
        private readonly IApiConnectionSettings apiConnectionSettings;
        private readonly OrdersFactory ordersFactory;
        private readonly ProductsFactory productsFactory;

        public OrdersBl(IApiConnectionSettings apiConnectionSettings)
        {
            this.apiConnectionSettings = apiConnectionSettings;
            ordersFactory = new OrdersFactory(apiConnectionSettings.GetConnectionString());
            productsFactory = new ProductsFactory(apiConnectionSettings.GetConnectionString());
        }

        public OrdersBl(string dbConnectionString)
        {
            ordersFactory = new OrdersFactory(dbConnectionString);
            productsFactory = new ProductsFactory(dbConnectionString);
        }

        public OrdersBl()
        {
            ordersFactory = new OrdersFactory("Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            productsFactory = new ProductsFactory("Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
        }

        public OrdersResponseCtrl GetTotal(int orderId)
        {
            var ordersResponse = new OrdersResponseCtrl { };
            try
            {
                ordersResponse.Total = ordersFactory.CalculateOrderTotal(orderId);
                ordersResponse.Status = ResponseStatus.Success;
                return ordersResponse;
            }
            catch (Exception E)
            {
                ordersResponse.Status = ResponseStatus.Error;
                ordersResponse.Error.ExceptionMessage = E.Message;
                ordersResponse.Error.ErrorMessage = $"There was an error while trying to calculate the total for order no. {orderId}";
                return ordersResponse;
            }
        }

        public string GetUserLatestOrder(string userId)
        {
            return ordersFactory.GetUserLatestOrder(userId);
        }

        public ProductResponseCtrl AddProducts(OrdersCtrl newOrder)
        {
            var productsResponse = new ProductResponseCtrl { Products = newOrder.Products };
            try
            {
                var products = productsFactory.Create(x =>
                {
                    x.OrderProducts = newOrder.Products;
                    x.OrderId = newOrder.Id;
                });
                if (!products.Add())
                    throw new Exception("There was a problem adding products to your order");
                productsResponse.Status = ResponseStatus.Success;
                return productsResponse;
            }
            catch (Exception E)
            {
                productsResponse.Error.ExceptionMessage = E.Message;
                productsResponse.Error.ErrorMessage = "There was a problem adding products to your order";
                productsResponse.Status = ResponseStatus.Error;
                return productsResponse;
            }
        }
        public OrdersResponseCtrl AddOrder(OrdersCtrl newOrder)
        {
            var response = new OrdersResponseCtrl { };
            try
            {
                var order = ordersFactory.Create(x =>
                {
                    x.Created = newOrder.Created;
                    x.Products = newOrder.Products;
                    x.UserId = newOrder.UserId;
                });

                var orderId = order.AddOrder();
                if (string.IsNullOrEmpty(orderId))
                {
                    throw new Exception("There was an error while trying to add your order");
                }
                response.Status = ResponseStatus.Success;
                response.OrderId = int.Parse(orderId);
                return response;
            }
            catch (Exception E)
            {
                response.Status = ResponseStatus.Error;
                response.Error.ExceptionMessage = E.Message;
                response.Error.StackTrace = E.StackTrace;
                return response;
            }
        }
    }
}

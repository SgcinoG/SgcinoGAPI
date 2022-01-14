using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using SgcinoGAPIDataLayer.Models;
using SgcinoGClient;
using SgcinoGAPIDataModels.Models;

namespace SgcinoGAPIClient
{
    class Program
    {
        public static string Username = "Sgcino";
        public static string Password = "PlzLetmein!";
        public static string Url = "https://localhost:44393/";
        public static string UserId = "8B5B0EDE-7A13-44C2-96EE-52FE42915838";
        public static int OrderNum = 0;
        public static APIClient apiClient;
        public static OrdersCtrl newOrder;
        static void Main(string[] args)
        {
            apiClient = new APIClient(Username, Password, Url);
            //apiClient = new apiClient(Username, Password, Url);
            RegisterUser();
            AddOrder();
            AddProducts();
            //GetOrderNum();
            //GetTotal();
            Console.ReadLine();
        }
        public static void RegisterUser()
        {
            var result = apiClient.RegisterUser(new UsersCtrl { Created = DateTime.Now, Name = "Admin", Surname = "Hello", Username = "Login" });
            if (result.Status == ResponseStatus.Success)
            {
                Console.WriteLine("Thank you, your registration has been successful");
            }
            else
            {
                Console.WriteLine(result.Error.ErrorMessage);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public static void AddOrder()
        {
            newOrder = new OrdersCtrl
            {
                Created = DateTime.Now,
                UserId = UserId
            };
            var response = apiClient.AddOrder(newOrder);
            if (response.Status == ResponseStatus.Success)
            {
                Console.WriteLine($"Order has been places successfully, Order No. {response.OrderId}");
                newOrder.Id = response.OrderId;
            }
            else
            {
                Console.WriteLine($"There was an error placing your order {response.Error.ExceptionMessage}");
            }
        }
        public static void AddProducts()
        {
            Console.WriteLine("Adding products to cart..");
            Random random = new Random();
            var limit = random.Next(1, 3);
            //builds a list of products
            List<int> products = new List<int>();
            for (var x = 0; x < limit; x++)
            {
                var prodId = random.Next(1, 5);
                products.Add(prodId);
            }
            newOrder.Products = products;
            var result = apiClient.AddProducts(newOrder);
            switch (result.Status)
            {
                case ResponseStatus.Success:
                    Console.WriteLine("Your order has been placed..");
                    break;
                case ResponseStatus.Error:
                    Console.WriteLine("Couldn't add products");
                    Console.WriteLine(result.Error.ErrorMessage);
                    break;
                default:
                    break;
            }
        }
        public static void GetTotal()
        {
            Console.WriteLine($"Your total is R{apiClient.GetTotal(OrderNum)}");
            Console.ReadLine();
        }
        public static void GetOrderNum()
        {
            var orderNum = JsonConvert.DeserializeObject(apiClient.GetUserLatestOrder(UserId));
            var temp = JsonConvert.DeserializeObject(orderNum.ToString());
            OrderNum = int.Parse(temp.ToString());
            Console.WriteLine($"Your order has been placed successfully, your order no. is #{OrderNum}");
        }
    }
}
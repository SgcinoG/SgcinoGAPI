using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataModels.Models;
using System.Collections.Generic;

namespace SgcinoGAPITests
{
    [TestClass]
    public class APITest
    {
        OrdersBl ordersBl;
        UsersBl usersBl;
        int OrderNum = 0;
        List<int> Products;
        string UserId = "53777B20-E221-40AB-A8D3-049FAE55775B";
        string ConnectionString = "Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        public APITest()
        {
            usersBl = new UsersBl(ConnectionString);
            ordersBl = new OrdersBl(ConnectionString);
        }

        [TestMethod]
        public void AddOrder()
        {
            var response = ordersBl.AddOrder(new OrdersCtrl { Created = DateTime.Now, UserId = UserId });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
            Assert.IsNotNull(response.OrderId);
        }

        [TestMethod]
        public void AddInvalidOrderNum()
        {
            Products = new List<int> { 1, 2 };
            var response = ordersBl.AddProducts(new OrdersCtrl { Id = 90, Created = DateTime.Now, UserId = UserId, Products = Products });
            Assert.IsFalse(ResponseStatus.Success == response.Status);
            Assert.IsTrue(response.Error.ErrorMessage == "There was a problem adding products to your order");
        }

        [TestMethod]
        public void AddProducts()
        {
            Products = new List<int> { 1, 2 };
            var response = ordersBl.AddProducts(new OrdersCtrl { Id = 1, Created = DateTime.Now, UserId = UserId, Products = Products });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
        }

        [TestMethod]
        public void AddInvalidProducts()
        {
            Products = new List<int> { 1, 25 };
            var response = ordersBl.AddProducts(new OrdersCtrl { Id = 1, Created = DateTime.Now, UserId = UserId, Products = Products });
            Assert.IsTrue(ResponseStatus.Error == response.Status);
            Assert.IsTrue(response.Error.ErrorMessage == "There was a problem adding products to your order");
        }

        [TestMethod]
        public void AddUser()
        {
            var response = usersBl.RegisterUser(new UsersCtrl { Created = DateTime.Now, Name = "Unit Test", Surname = "Testing", Username = "Awee" });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
        }

        [TestMethod]
        public void GetTotal()
        {
            OrderNum = 21;
            var response = ordersBl.GetTotal(OrderNum);
            Assert.IsTrue(ResponseStatus.Success == response.Status);
            Assert.IsNotNull(response.Total);
        }

        [TestMethod]
        public void GetInvalidOrderTotal()
        {
            OrderNum = 99;
            var response = ordersBl.GetTotal(OrderNum);
            Assert.IsTrue(ResponseStatus.Error == response.Status);
            Assert.AreEqual(response.Error.ErrorMessage, $"There was an error while trying to calculate the total for order no. {OrderNum}");
        }
    }
}
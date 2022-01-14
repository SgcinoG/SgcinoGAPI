using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataLayer.Models;
using SgcinoGAPIDataModels.Models;
using System.Collections.Generic;

namespace SgcinoGAPITests
{
    [TestClass]
    public class UnitTest1
    {
        OrdersBl ordersBl;
        UsersBl usersBl;
        public UnitTest1()
        {
            usersBl = new UsersBl("Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            ordersBl = new OrdersBl("Server=192.168.2.181;Initial Catalog=SgcinoGAPIDB;Persist Security Info=False;User ID=sa;Password=Contour1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
        }
        [TestMethod]
        public void AddOrder()
        {
            var response = ordersBl.AddOrder(new OrdersCtrl { Created = DateTime.Now, UserId = "53777B20-E221-40AB-A8D3-049FAE55775B" });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
        }
        [TestMethod]
        public void AddInvalidOrderNum()
        {
            var response = ordersBl.AddProducts(new OrdersCtrl { Id = 90, Created = DateTime.Now, UserId = "53777B20-E221-40AB-A8D3-049FAE55775B", Products = new List<int> { 1, 2 } });
            Assert.IsFalse(ResponseStatus.Success == response.Status);
        }
        [TestMethod]
        public void AddProducts()
        {
            var response = ordersBl.AddProducts(new OrdersCtrl { Id = 1, Created = DateTime.Now, UserId = "53777B20-E221-40AB-A8D3-049FAE55775B", Products = new List<int> { 1, 2 } });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
        }
        [TestMethod]
        public void AddUser()
        {
            var response = usersBl.RegisterUser(new UsersCtrl { Created = DateTime.Now, Name = "Unit Test", Surname = "Testing", Username = "Awee" });
            Assert.IsTrue(ResponseStatus.Success == response.Status);
        }
    }
}
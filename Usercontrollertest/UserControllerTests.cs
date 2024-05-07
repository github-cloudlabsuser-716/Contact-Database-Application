using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Usercontrollertest
{

    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _users;

        [TestInitialize]
        public void Setup()
        {
            _users = new List<User>
        {
            new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
            new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
            // Add more users as needed
        };

            UserController.userlist = _users;
            _controller = new UserController();
        }

        [TestMethod]
        public void Index_ReturnsCorrectViewWithUsers()
        {
            var result = _controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(_users.Count, model.Count);
        }

        [TestMethod]
        public void Details_ReturnsCorrectViewWithUser()
        {
            var result = _controller.Details(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual(_users[0], model);
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            var newUser = new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(UserController.userlist.Contains(newUser));
        }

        [TestMethod]
        public void Edit_ValidUser_RedirectsToIndex()
        {
            var user = _users[0];
            user.Name = "Updated User";
            var result = _controller.Edit(user.Id, user) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated User", UserController.userlist.First(u => u.Id == user.Id).Name);
        }

        [TestMethod]
        public void Delete_ValidUser_RedirectsToIndex()
        {
            var user = _users[0];
            var result = _controller.Delete(user.Id, null) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Contains(user));
        }
    }
}
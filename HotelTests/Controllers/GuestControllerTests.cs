using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Hotel.Data;
using Moq;
using Hotel.Data.Models;
using System.Threading.Tasks;
using Hotel.Services.Interfaces;
using Hotel.Models;
using AutoMapper;
using Hotel.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Hotel.Controllers.Tests
{
    [TestClass()]
    public class GuestsControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var mockS = new Mock<IGuestService>();
            GuestController guestController = new GuestController(mockS.Object);

            var result = guestController.Index().Result as ViewResult;

            Assert.IsTrue(result.ViewData.ContainsKey("Succes"));
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var mockS = new Mock<IGuestService>();
            GuestViewModel guest = new GuestViewModel() { LastName = "Hasan" };
            mockS.Setup(p => p.GetGuest(1)).ReturnsAsync(guest);
            GuestController guestController = new GuestController(mockS.Object);

            var result = guestController.Details(1).Result as ViewResult;

            Assert.AreEqual("Hasan", (result.Model as GuestViewModel).LastName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var mockS = new Mock<IGuestService>();
            GuestViewModel guest = new GuestViewModel { LastName = "Hasan", FirstName = "Test", Address = "test", PostCode = "44", Country = "nl", Town = "grt", PhoneNumber = "123", Email = "nnn" };
            mockS.Setup(p => p.AddGuest(guest)).Returns(7);
            GuestController guestController = new GuestController(mockS.Object);

            var result =  guestController.Create(guest).Result as ViewResult;

            Assert.IsTrue(result.ViewData.Values.Contains("The guest Hasan has registered succesfully!"));
        }
    }
}
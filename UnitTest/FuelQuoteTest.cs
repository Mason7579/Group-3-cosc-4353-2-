using System;
using System.Collections.Generic;
using cosc_4353_project.Controllers;
using cosc_4353_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Npgsql;
using Xunit;

namespace UnitTest
{
    public class FuelQuoteControllerTest
    {
        [Fact]
        public void Test_History_ReturnsView()
        {
            var controller = new FuelQuoteController();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = "username_cookie=admin"; 
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = controller.history() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Test_FuelQuoteForm_ReturnsView()
        {

            var controller = new FuelQuoteController();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = "username_cookie=admin"; 
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = controller.FuelQuoteForm() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Test_FuelQuoteFormDB_RedirectsToHistory()
        {
            var controller = new FuelQuoteController();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = "username_cookie=admin"; 
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var gallonsRequested = 100.0;
            var deliveryAddress = "123 Main St";
            var deliveryDate = "12/08/2022";
            var suggestedPrice = 2.90;
            var totalAmountDue = 290.0;
            var result = controller.FuelQuoteFormDB(gallonsRequested, deliveryAddress, deliveryDate, suggestedPrice, totalAmountDue) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("history", result.ActionName);
            Assert.Equal("FuelQuote", result.ControllerName);
        }
    }
}

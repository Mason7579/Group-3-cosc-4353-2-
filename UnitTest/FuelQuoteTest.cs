using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class FuelQuoteTest
    {
        [Fact]
        public void Test_FuelQuoteHistory_return_history()
        {
            var controller = new FuelQuoteController();
            var result = controller.history() as ViewResult;
            var historyList = result.Model as FuelQuoteHistory;
            var fuelQuote = historyList.History[0];
            Assert.Equal(139, fuelQuote.Gallons_Requested);
            Assert.Equal("12/08/2022", fuelQuote.Delivery_Date);
            Assert.Equal(2.90, fuelQuote.Suggested_Price);
            Assert.Equal(403.1, fuelQuote.Total_Amount_Due);
            Assert.Equal("123 HelloWorld St. TX", fuelQuote.Delivery_Address);
        }
    }
}

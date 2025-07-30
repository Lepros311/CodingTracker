using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodingTracker.UnitTests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void ValidateDate_MMddyyyFormat_ReturnsInputtedDate()
        {
            var validation = new Validation();

            string date = "12/05/2024";

            var result = validation.ValidateDate(date);

            Assert.Equals(date, result);
        }
    }
}

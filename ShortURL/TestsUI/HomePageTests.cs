using NUnit.Framework;
using OpenQA.Selenium;
using ShortURL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.Tests
{
    public class HomePageTests : BaseTests
    {
        const int startNumberOfUrls = 3;
        const int startNumberOfVisitors = 289;
        [Test]
        public void VerrifyHomePageElements()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            int numberOfUrls = homePage.NumberOfUrls();
            int numberOfVisitors = homePage.NumberOfVisitors();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("URL Shortener", homePage.GetPageTitle(), "Expected title of page is different than actual.");
                Assert.AreEqual("URL Shortener", homePage.GetPageHeader(), "Expected header of page is different than actual." );
                Assert.IsTrue(homePage.IsPageOpen(), "Home page is not open.");
                Assert.IsTrue(numberOfUrls >= startNumberOfUrls, "Number of URLs is not correct.");
                Assert.IsTrue( numberOfVisitors >= startNumberOfVisitors, "Number of visitors is not correct.");
            });

        }
    }
}

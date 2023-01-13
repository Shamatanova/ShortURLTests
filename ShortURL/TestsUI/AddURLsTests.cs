
using NUnit.Framework;
using OpenQA.Selenium;
using ShortURL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.TestsUI
{
    public class AddURLsTests : BaseTests
    {

        string testUrl = GenerateUniqueUrl();
        string newTestUrl = GenerateUniqueUrl();
        string testShortCode = GenerateUniqueShortCode();
        string newTestShortCode = GenerateUniqueShortCode();
        string testShortCodeWithSpecialSymbols = GenerateUniqueShortCodeWithSpecialSymbols();
        string testUrlWithSpecialSymbols = GenerateUniqueUrlWithSpecialSymbols();

        const string alreadyUsedUrl = "https://selenium.dev";
        const string alreadyUsedShortCode = "nak";



        [Test]
        public void VerrifyAddUrlsPageElements()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Add Short URL", addUrlPage.GetPageTitle(), 
                                "Expected title of page is different than actual.");
                Assert.AreEqual("Add Short URL", addUrlPage.GetPageHeader(), 
                                "Expected header of page is different than actual.");
                Assert.IsTrue(addUrlPage.IsPageOpen(), 
                              "Add Short URL page is not open.");
                Assert.IsTrue(addUrlPage.InputUrlBox.Displayed, 
                              "URL Box is not displayed.");
                Assert.IsTrue(addUrlPage.InputShortCodeBox.Displayed, 
                              "Short Code Box is not displayed.");
                Assert.IsTrue(addUrlPage.CreateButton.Displayed, 
                              "Create Button is not displayed.");
            });
        }

        [Test]
        public void CreateNewShortUrlWithCorrectURLAndCorrectShortCode()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            int numberOfShortURLs = homePage.NumberOfUrls();

            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrl);
            addUrlPage.EnterShortCodeField(testShortCode);
            addUrlPage.ClickOnCreateButton();

            var shortUrls = driver.FindElements((By.XPath("//tr")));

            bool elementFound = false;

            foreach (var item in shortUrls)
            {
                if (item.Text.Contains(testUrl) && item.Text.Contains(testShortCode))
                {
                    elementFound = true;
                }
            }

            Assert.That(elementFound, "No record found.");

            Assert.AreEqual("Short URLs", addUrlPage.GetPageTitle(), 
                            "Expected Title of page is different than actual.");

            addUrlPage.LinkHomePage.Click();
            int numberOfShortUrls = int.Parse(driver.FindElement(By.XPath
                                    ("//li[1]/text()/following-sibling::b")).Text);

            Assert.That(numberOfShortURLs < numberOfShortUrls, 
                        "Actual number of Short URLs is not correct");
        }

        [Test]
        public void TryToCreateNewShortURLWithSameURLAndShortCode()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrl);
            addUrlPage.EnterShortCodeField(testShortCode);
            addUrlPage.ClickOnCreateButton();

            Assert.That(addUrlPage.ErrorMessageIsVisible(), 
                        "Expected error message is not visible.");
            Assert.AreEqual("Short code already exists!", addUrlPage.ErrorMessage(), 
                            "Expected error message is different than actual.");
        }

        [Test]
        public void TryToCreateNewShortURLWithSameURLAndDifferentShortCode()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            int numberOfShortURLs = homePage.NumberOfUrls();

            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrl);
            addUrlPage.EnterShortCodeField(newTestShortCode);
            addUrlPage.ClickOnCreateButton();

            var shortUrls = driver.FindElements((By.XPath("//tr")));
            
            bool isItFind = false;

            foreach (var item in shortUrls)
            {
                if (item.Text.Contains(testUrl) && item.Text.Contains(newTestShortCode))
                {
                    isItFind = true;
                }
            }

            Assert.That(isItFind, "No record");

            addUrlPage.LinkHomePage.Click();
            int newNumberOfShortURLs = int.Parse(driver.FindElement(By.XPath
                                       ("//li[1]/text()/following-sibling::b")).Text);

            Assert.That(numberOfShortURLs < newNumberOfShortURLs, 
                        "Numbers of Short URLs are different than expected.");

        }

        [Test]
        public void TryToCreateNewShortURLWithDifferentURLAndSameShortCode()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            int numberOfShortUrls = homePage.NumberOfUrls();

            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(newTestUrl);
            addUrlPage.EnterShortCodeField(testShortCode);
            addUrlPage.ClickOnCreateButton();

            Assert.That(addUrlPage.ErrorMessageIsVisible(), 
                        "Error message is not visible.");
            Assert.AreEqual("Short code already exists!", addUrlPage.ErrorMessage(),
                            "Expected error message is different than actual.");

            addUrlPage.LinkShortUrlsPage.Click();            
            var shortUrls = driver.FindElements((By.XPath("//tr")));

            bool isItFind = false;

            foreach (var item in shortUrls)
            {
                if (item.Text.Contains(newTestUrl) && item.Text.Contains(testShortCode))
                {
                    isItFind = true;
                }
            }

            Assert.IsFalse(isItFind, "No record");

            addUrlPage.LinkHomePage.Click();
            var actualNumberOfUrls = int.Parse(driver.FindElement(By.XPath
                                     ("//li[1]/text()/following-sibling::b")).Text);

            Assert.That(numberOfShortUrls == actualNumberOfUrls, 
                        "Expected number of Short URLs are not equal than actual.");

        }

        [Test]
        public void TryToCreateNewShortURLWithMissingURL()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(string.Empty);
            addUrlPage.EnterShortCodeField(testShortCode);
            addUrlPage.ClickOnCreateButton();

            Assert.IsTrue(addUrlPage.ErrorMessageIsVisible(), 
                          "Error message is not visible.");
            Assert.AreEqual("URL cannot be empty!", addUrlPage.ErrorMessage(), 
                            "Eror message is not correct.");
        }

        [Test]
        public void TryToCreateNewShortURLWithMissingShortCode()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrl);
            addUrlPage.EnterShortCodeField(string.Empty);
            addUrlPage.ClickOnCreateButton();

            Assert.IsTrue(addUrlPage.ErrorMessageIsVisible(), 
                          "Error message is not visible.");
            Assert.AreEqual("Short code cannot be empty!", addUrlPage.ErrorMessage(),
                            "Expected Error message is different than actual.");

        }

        [Test]
        public void TryToCreateNewShortURLWithSpecialSymbolsInURL()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            int numberOfShortURLs = homePage.NumberOfUrls();
            
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrlWithSpecialSymbols);
            addUrlPage.EnterShortCodeField(GenerateUniqueShortCode());
            addUrlPage.ClickOnCreateButton();

            addUrlPage.LinkHomePage.Click();
            var actualNumberOfUrls = int.Parse(driver.FindElement(By.XPath
                                     ("//li[1]/text()/following-sibling::b")).Text);

            Assert.That(numberOfShortURLs < actualNumberOfUrls,
                        "Expected number of Short URLs are different.");
        }

        [Test]
        public void TryToCreateNewShortURLWithSpecialSymbolsInShortCode()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(testUrl);
            addUrlPage.EnterShortCodeField(testShortCodeWithSpecialSymbols);
            addUrlPage.ClickOnCreateButton();

            Assert.That(addUrlPage.ErrorMessageIsVisible(),
                        "Error message is not visible.");
            Assert.AreEqual("Short code holds invalid chars!", addUrlPage.ErrorMessage(),
                            "Expected Error message is different than actual.");
        }

        [Test]
        public void TryToCreateNewShortURLWithAlreadyUsedShortCodeAndURL()
        {
            var addUrlPage = new AddURLsPage(driver);
            addUrlPage.Open();

            addUrlPage.EnterUrlField(alreadyUsedUrl);
            addUrlPage.EnterShortCodeField(alreadyUsedShortCode);
            addUrlPage.ClickOnCreateButton();

            Assert.That(addUrlPage.ErrorMessageIsVisible(),
                        "Error message is not visible.");
            Assert.AreEqual("Short code already exists!", addUrlPage.ErrorMessage(),
                            "Expected Error message is different than actual.");
        }

        public static string GenerateUniqueUrl()
        {
            var result = new StringBuilder("https://shami");

            var random = new Random();
            int number = random.Next();
            result.Append(number);

            result.Append(".com");

            return result.ToString();
        }

        public static string GenerateUniqueShortCode()
        {
            var result = new StringBuilder();

            result.Append("shami");

            var random = new Random();
            int number = random.Next();
            result.Append(number);

            return result.ToString();
        }

        public static string GenerateUniqueUrlWithSpecialSymbols()
        {
            var result = new StringBuilder("https://shami");

            var specialSymbols = "*";
            result.Append(specialSymbols);

            var random = new Random();
            int number = random.Next();
            result.Append(number);

            result.Append(".com");

            return result.ToString();
        }

        public static string GenerateUniqueShortCodeWithSpecialSymbols()
        {
            var result = new StringBuilder();

            result.Append("shami");

            var specialSymbols = "*";
            result.Append(specialSymbols);

            var random = new Random();
            int number = random.Next();
            result.Append(number);

            return result.ToString();
        }
    }

   
}

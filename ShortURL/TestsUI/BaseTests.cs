using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ShortURL.Objects;

namespace ShortURL
{
    public class BaseTests
    {
       protected IWebDriver driver;
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
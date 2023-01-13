using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.Objects
{
    public class BasePage
    {
        public IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public virtual string PageURL { get; }
        public IWebElement LinkHomePage => driver.FindElement(By.XPath("//a[contains(.,'Home')]"));
        public IWebElement LinkShortUrlsPage => driver.FindElement(By.XPath("//a[contains(.,'Short URLs')]"));
        public IWebElement LinkAddUrlPage => driver.FindElement(By.XPath("//a[contains(.,'Add URL')]"));
        public IWebElement HeaderPageName => driver.FindElement(By.CssSelector("body > main > h1"));

        public void Open()
        {
            driver.Navigate().GoToUrl(PageURL);
            
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeader()
        {
            return HeaderPageName.Text;
        }

        public bool IsPageOpen()
        {
            if (driver.Url == PageURL)
            {
                return true;
            }
            return false;
        }
    }
}

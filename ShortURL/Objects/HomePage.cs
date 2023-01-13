using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.Objects
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageURL => "http://localhost:8080/";

        public IWebElement ShortUrlsCount => driver.FindElement(By.XPath("//li[1]/text()/following-sibling::b"));
        public IWebElement VisitorsCount => driver.FindElement(By.XPath("//li[2]/text()/following-sibling::b"));

        public int NumberOfUrls()
        {
            string numbers = ShortUrlsCount.Text;
            return int.Parse(numbers);
        }
        public int NumberOfVisitors()
        {
            string numbers = VisitorsCount.Text;
            return int.Parse(numbers);
        }
    }
}

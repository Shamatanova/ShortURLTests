using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.Objects
{
    public class ShortURLsPage : BasePage
    {
        public ShortURLsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageURL => "http://localhost:8080/urls";
        
        public ReadOnlyCollection<IWebElement> ShortUrls => driver.FindElements(By.XPath("//tr"));

       


    }
}

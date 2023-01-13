using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.Objects
{
    public class AddURLsPage : BasePage
    {
        public AddURLsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageURL => "http://localhost:8080/add-url";

        public IWebElement InputUrlBox => driver.FindElement(By.XPath("//input[@id='url']"));
        public IWebElement InputShortCodeBox => driver.FindElement(By.XPath("//input[@id='code']"));
        public IWebElement CreateButton => driver.FindElement(By.XPath("//button[contains(.,'Create')]"));

        public IWebElement ErrorField => driver.FindElement(By.XPath("//div[@class='err']"));

        public void ClickOnCreateButton()
        {
            CreateButton.Click();
        }

        public bool ErrorMessageIsVisible()
        {
            if (ErrorField.Displayed)
            {
                return true;
            }
            return false;
        }

        public string ErrorMessage()
        {
            string text = ErrorField.Text;
            return text;
        }

        public void EnterUrlField(string testUrl)
        {
            InputUrlBox.Click();
            InputUrlBox.Clear();
            InputUrlBox.SendKeys(testUrl);
        }
        public void EnterShortCodeField(string testShortCode)
        {
            InputShortCodeBox.Click();
            InputShortCodeBox.Clear();
            InputShortCodeBox.SendKeys(testShortCode);
        }
    }
}

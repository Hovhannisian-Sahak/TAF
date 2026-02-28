using OpenQA.Selenium;
using TAF.Core.WebDriver;

namespace TAF.Core.WebElement
{
    public class BaseElement
    {
        protected IWebElement Element(By locator)
        {
            return WebDriverWrapper.Driver.FindElement(locator);
        }
    }
}
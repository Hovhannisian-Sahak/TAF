using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class Link : WebElement.BaseElement
    {
        private readonly By _locator;

        public Link(By locator)
        {
            _locator = locator;
        }

        public string Href => Element(_locator).GetAttribute("href");

        public void Click()
        {
            Element(_locator).Click();
        }
    }
}
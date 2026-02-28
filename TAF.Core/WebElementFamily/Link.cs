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

        public string Text =>
            Element(_locator).Text;

        public string Href =>
            Element(_locator).GetAttribute("href");

        public void Click()
        {
            Retry(() =>
            {
                WaitUntilClickable(_locator);
                Element(_locator).Click();
            });
        }

        public void ClickAndWaitForUrl(string urlPart)
        {
            Click();
            WaitUntilUrlContains(urlPart);
        }
    }
}
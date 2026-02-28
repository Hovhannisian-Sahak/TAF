using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class Button : WebElement.BaseElement
    {
        private readonly By _locator;

        public Button(By locator)
        {
            _locator = locator;
        }

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
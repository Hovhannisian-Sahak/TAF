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
            Log.Info($"Click button: {_locator}");
            Retry(() =>
            {
                WaitUntilClickable(_locator);
                Element(_locator).Click();
            });
        }

        public void ClickAndWaitForUrl(string urlPart)
        {
            Log.Info($"Click button and wait for URL containing '{urlPart}': {_locator}");
            Click();
            WaitUntilUrlContains(urlPart);
        }
    }
}

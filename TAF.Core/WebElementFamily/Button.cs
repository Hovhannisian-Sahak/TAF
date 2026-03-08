using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
                ScrollIntoView(_locator);
                Element(_locator).Click();
            });
        }

        public void ClickWithActions()
        {
            Log.Info($"Click button with actions: {_locator}");
            Retry(() =>
            {
                var element = Element(_locator);
                ScrollIntoView(_locator);
                new Actions(Driver).MoveToElement(element).DoubleClick().Perform();
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

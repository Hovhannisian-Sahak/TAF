using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
            Log.Info($"Click link: {_locator}");
            Retry(() =>
            {
                var element = VisibleElement(_locator);
                ScrollIntoView(element);
                element.Click();
            });
        }
        public void ScrollToElementAndClick()
        {
            Log.Info($"Click link: {_locator}");

            Retry(() =>
            {
                var element = WaitUntilClickable(_locator); // finds, scrolls, waits
                element.Click();
            });
        }
        public void HoverToElement()
        {
            Log.Info($"Click link with actions: {_locator}");
            Retry(() =>
            {
                var element = VisibleElement(_locator);
                ScrollIntoView(element);
                new Actions(Driver).MoveToElement(element).Perform();
            });
        }

        public void Hover()
        {
            Log.Info($"Hover link: {_locator}");
            Retry(() =>
            {
                ScrollIntoView(_locator);
                Hover(_locator);
            });
        }
        public void ClickAndWaitForUrl(string urlPart)
        {
            Log.Info($"Click link and wait for URL containing '{urlPart}': {_locator}");
            Click();
            WaitUntilUrlContains(urlPart);
        }
        
    }
}

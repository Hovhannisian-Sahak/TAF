using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class CheckBox : WebElement.BaseElement
    {
        private readonly By _locator;

        public CheckBox(By locator)
        {
            _locator = locator;
        }

        public bool IsChecked => Element(_locator).Selected;

        public void Check()
        {
            Log.Info($"Check checkbox: {_locator}");
            Retry(() =>
            {
                ToggleTo(true);
            });
        }

        public void Uncheck()
        {
            Log.Info($"Uncheck checkbox: {_locator}");
            Retry(() =>
            {
                ToggleTo(false);
            });
        }

        private void ToggleTo(bool shouldBeChecked)
        {
            var element = Element(_locator);
            if (element.Selected == shouldBeChecked)
                return;

            ScrollIntoView(_locator);

            try
            {
                element.Click();
            }
            catch (WebDriverException)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
            }

            if (Element(_locator).Selected == shouldBeChecked)
                return;

            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", Element(_locator));

            if (Element(_locator).Selected != shouldBeChecked)
                throw new InvalidOperationException($"Failed to set checkbox '{_locator}' to '{shouldBeChecked}'.");
        }
        
    }
}

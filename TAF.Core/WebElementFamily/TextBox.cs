using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class TextBox : WebElement.BaseElement
    {
        private readonly By _locator;

        public TextBox(By locator)
        {
            _locator = locator;
        }

        public void EnterText(string text)
        {
            Log.Info($"Enter text into textbox: {_locator}");
            Retry(() =>
            {
                ScrollIntoView(_locator);
                var element = WaitUntilClickable(_locator);
                try
                {
                    element.Clear();
                }
                catch (WebDriverException)
                {
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].value='';", element);
                }

                try
                {
                    element.SendKeys(text);
                }
                catch (WebDriverException)
                {
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].focus(); arguments[0].value=arguments[1];", element, text);
                }
            });
        }

        public void EnterTextAndWait(string value)
        {
            Log.Info($"Enter text and wait for value '{value}' in textbox: {_locator}");
            EnterText(value);
            WaitUntilAttributeContains(_locator, "value", value);
        }
        
    }
}

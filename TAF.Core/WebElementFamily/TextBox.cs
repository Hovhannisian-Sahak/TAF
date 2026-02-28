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
                var element = Element(_locator);
                element.Clear();
                element.SendKeys(text);
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

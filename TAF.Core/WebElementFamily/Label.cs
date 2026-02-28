using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class Label : WebElement.BaseElement
    {
        private readonly By _locator;

        public Label(By locator)
        {
            _locator = locator;
        }

        public string Text =>
            Element(_locator).Text;

        public void WaitUntilText(string expectedText)
        {
            Log.Info($"Wait until label text contains '{expectedText}': {_locator}");
            WaitUntilTextContains(_locator, expectedText);
        }
    }
}

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

        public string Text => Element(_locator).Text;
    }
}
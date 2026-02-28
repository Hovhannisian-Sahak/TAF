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
            var element = Element(_locator);
            element.Clear();
            element.SendKeys(text);
        }

        public string Value => Element(_locator).GetAttribute("value");
    }
}
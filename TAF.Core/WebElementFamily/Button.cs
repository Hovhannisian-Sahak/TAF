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
            Element(_locator).Click();
        }
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TAF.Core.WebElementFamily
{
    public class Dropdown : WebElement.BaseElement
    {
        private readonly By _locator;

        public Dropdown(By locator)
        {
            _locator = locator;
        }

        private SelectElement Select =>
            new SelectElement(Element(_locator));

        public void SelectByText(string text)
        {
            Select.SelectByText(text);
        }

        public void SelectByValue(string value)
        {
            Select.SelectByValue(value);
        }

        public string SelectedOption =>
            Select.SelectedOption.Text;
    }
}
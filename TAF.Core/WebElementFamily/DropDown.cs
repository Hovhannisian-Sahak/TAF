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
            Retry(() => Select.SelectByText(text));
        }

        public void SelectByValue(string value)
        {
            Retry(() => Select.SelectByValue(value));
        }

        public void SelectByIndex(int index)
        {
            Retry(() => Select.SelectByIndex(index));
        }

        public string SelectedOption =>
            Select.SelectedOption.Text;

        public void WaitUntilSelected(string expectedText)
        {
            WaitUntilTextContains(_locator, expectedText);
        }
    }
}
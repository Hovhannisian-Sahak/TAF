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
            Log.Info($"Select dropdown option by text '{text}': {_locator}");
            Retry(() => Select.SelectByText(text));
        }

        public void SelectByValue(string value)
        {
            Log.Info($"Select dropdown option by value '{value}': {_locator}");
            Retry(() => Select.SelectByValue(value));
        }

        public void SelectByIndex(int index)
        {
            Log.Info($"Select dropdown option by index '{index}': {_locator}");
            Retry(() => Select.SelectByIndex(index));
        }

        public string SelectedOption =>
            Select.SelectedOption.Text;

        public void WaitUntilSelected(string expectedText)
        {
            Log.Info($"Wait until dropdown selected option contains '{expectedText}': {_locator}");
            WaitUntilTextContains(_locator, expectedText);
        }
    }
}

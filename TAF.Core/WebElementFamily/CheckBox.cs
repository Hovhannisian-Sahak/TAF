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
                if (!IsChecked)
                    Element(_locator).Click();
            });
        }

        public void Uncheck()
        {
            Log.Info($"Uncheck checkbox: {_locator}");
            Retry(() =>
            {
                if (IsChecked)
                    Element(_locator).Click();
            });
        }
    }
}

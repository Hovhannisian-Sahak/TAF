using OpenQA.Selenium;

namespace TAF.Core.WebDriver
{
    public static class WebDriverWrapper
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    throw new InvalidOperationException("WebDriver is not initialized.");

                return _driver;
            }
        }

        public static void Init(BrowserType browser)
        {
            if (_driver != null)
                return;

            _driver = WebDriverFactory.Create(browser);
        }

        public static void Quit()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
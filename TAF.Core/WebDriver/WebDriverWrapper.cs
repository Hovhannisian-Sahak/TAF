using OpenQA.Selenium;
using System;

namespace TAF.Core.WebDriver
{
    public static class WebDriverWrapper
    {
        private static readonly ThreadLocal<IWebDriver> _driver =
            new ThreadLocal<IWebDriver>();

        public static IWebDriver Driver
        {
            get
            {
                if (!_driver.IsValueCreated || _driver.Value == null)
                    throw new InvalidOperationException("WebDriver not initialized for this thread.");

                return _driver.Value;
            }
        }

        public static void Init(IWebDriver driver)
        {
            if (_driver.IsValueCreated && _driver.Value != null)
                return;

            _driver.Value = driver;
        }

        public static void Quit()
        {
            if (_driver.IsValueCreated && _driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
                _driver.Value = null;
            }
        }
    }
}
using System;
using log4net;
using OpenQA.Selenium;

namespace TAF.Core.WebDriver
{
    public static class WebDriverWrapper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebDriverWrapper));

        private static readonly ThreadLocal<IWebDriver?> DriverThread =
            new ThreadLocal<IWebDriver?>();

        public static IWebDriver Driver
        {
            get
            {
                if (!DriverThread.IsValueCreated || DriverThread.Value == null)
                    throw new InvalidOperationException("WebDriver not initialized for this thread.");

                return DriverThread.Value;
            }
        }

        public static void Init(IWebDriver driver)
        {
            if (DriverThread.IsValueCreated && DriverThread.Value != null)
            {
                Log.Debug("WebDriver is already initialized for current thread. Init skipped.");
                return;
            }

            DriverThread.Value = driver;
            Log.Info("WebDriver initialized for current thread.");
        }

        public static void Quit()
        {
            if (!DriverThread.IsValueCreated || DriverThread.Value == null)
            {
                Log.Debug("WebDriver is not initialized for current thread. Quit skipped.");
                return;
            }

            Log.Info("Quit WebDriver for current thread.");
            DriverThread.Value.Quit();
            DriverThread.Value.Dispose();
            DriverThread.Value = null;
        }
    }
}

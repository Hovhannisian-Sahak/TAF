using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TAF.Core.Configuration;

namespace TAF.Core.WebDriver
{
    public static class WebDriverFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebDriverFactory));

        public static IWebDriver Create(BrowserType browser)
        {
            Log.Info($"Create WebDriver for browser: {browser}");

            return browser switch
            {
                BrowserType.Chrome => CreateChrome(),
                BrowserType.Firefox => CreateFirefox(),
                BrowserType.Edge => CreateEdge(),
                _ => throw new ArgumentException($"Unsupported browser: {browser}")
            };
        }

        private static IWebDriver CreateChrome()
        {
            Log.Debug("Initialize ChromeDriver with --start-maximized.");
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefox()
        {
            Log.Debug("Initialize FirefoxDriver.");
            var options = new FirefoxOptions();
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdge()
        {
            Log.Debug("Initialize EdgeDriver.");
            var options = new EdgeOptions();
            return new EdgeDriver(options);
        }
    }
}

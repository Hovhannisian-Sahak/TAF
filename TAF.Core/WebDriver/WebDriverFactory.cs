using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using TAF.Core.Configuration;

namespace TAF.Core.WebDriver
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(BrowserType browser)
        {
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
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefox()
        {
            var options = new FirefoxOptions();
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdge()
        {
            var options = new EdgeOptions();
            return new EdgeDriver(options);
        }
    }
}
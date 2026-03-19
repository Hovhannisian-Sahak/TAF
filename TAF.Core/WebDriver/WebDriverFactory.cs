using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TAF.Core.Configuration;
using TAF.Core.Logging;

namespace TAF.Core.WebDriver
{
    public static class WebDriverFactory
    {
        private static readonly log4net.ILog Log = AppLogger.For(typeof(WebDriverFactory));

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
            Log.Debug("Initializing ChromeDriver for CI / headless mode");

            var options = new ChromeOptions();

            // Headless + realistic viewport (critical for layout & visibility)
            options.AddArgument("--headless=new");              // modern headless (preferred since Chrome ~109)
            options.AddArgument("--window-size=1920,1080");     // desktop resolution – prevents mobile layout
            options.AddArgument("--disable-gpu");               // almost always needed in headless CI
            options.AddArgument("--no-sandbox");                // required in most containers / Jenkins agents
            options.AddArgument("--disable-dev-shm-usage");     // avoids /dev/shm crashes in limited-memory envs

            // Hide automation flags (some sites change behavior if they detect webdriver)
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36");

            // Extra stability flags
            options.AddArgument("--disable-infobars");          // no "Chrome is being controlled" bar
            options.AddArgument("--disable-extensions");        // clean profile
            options.AddArgument("--disable-notifications");

            // Debugging helpers (can be removed later)
            // options.AddArgument("--enable-logging");
            // options.AddArgument("--v=1");

            // Important: do NOT use --start-maximized in headless
            // It is ignored and can cause confusing behavior

            // Optional: if you still get MoveTargetOutOfBoundsException on hover
            // options.PageLoadStrategy = PageLoadStrategy.Normal;

            try
            {
                var driver = new ChromeDriver(options);
                Log.Info("ChromeDriver created successfully");
                return driver;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to create ChromeDriver", ex);
                throw;
            }
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

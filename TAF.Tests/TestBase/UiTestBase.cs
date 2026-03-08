using System.Text.RegularExpressions;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;

namespace TAF.Tests.TestBase;

public abstract class UiTestBase
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(UiTestBase));

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EnsureLogDirectory();
        Log.Info($"Starting test fixture: {TestContext.CurrentContext.Test.Name}");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Log.Info($"Finished test fixture: {TestContext.CurrentContext.Test.Name}");
    }

    [SetUp]
    public void SetUp()
    {
        Log.Info($"Starting test: {TestContext.CurrentContext.Test.FullName} (Worker {TestContext.CurrentContext.WorkerId})");
        var driver = WebDriverFactory.Create(Configuration.BrowserType);
        WebDriverWrapper.Init(driver);

        if (Configuration.BrowserOptions.ImplicitWait > 0)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Configuration.BrowserOptions.ImplicitWait);
        }

        if (Configuration.BrowserOptions.PageLoadTimeout > 0)
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Configuration.BrowserOptions.PageLoadTimeout);
        }
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = SaveFailureScreenshot();
                if (!string.IsNullOrWhiteSpace(path))
                {
                    Log.Warn($"Failure screenshot saved: {path}");
                }
            }
        }
        finally
        {
            WebDriverWrapper.Quit();
            Log.Info($"Finished test: {TestContext.CurrentContext.Test.FullName} - {TestContext.CurrentContext.Result.Outcome.Status}");
        }
    }

    private static string? SaveFailureScreenshot()
    {
        ITakesScreenshot? screenshotDriver;
        try
        {
            screenshotDriver = WebDriverWrapper.Driver as ITakesScreenshot;
        }
        catch (InvalidOperationException)
        {
            return null;
        }

        if (screenshotDriver == null)
        {
            return null;
        }

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
        var safeTestName = Regex.Replace(TestContext.CurrentContext.Test.Name, "[^a-zA-Z0-9_-]", "_");

        var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", "screenshots");
        Directory.CreateDirectory(screenshotsDir);

        var screenshotPath = Path.Combine(screenshotsDir, $"{safeTestName}_{timestamp}.png");
        try
        {
            screenshotDriver.GetScreenshot().SaveAsFile(screenshotPath);
            TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
            return screenshotPath;
        }
        catch (WebDriverException)
        {
            // Ignore screenshot failures when browser session is already closed/crashed.
            Log.Warn("Failed to capture screenshot because the browser session is unavailable.");
            return null;
        }
    }

    private static void EnsureLogDirectory()
    {
        var logsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", "logs");
        Directory.CreateDirectory(logsDir);
    }
}

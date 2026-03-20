using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Core.WebDriver;

namespace TAF.Tests.Hooks;

[Binding]
public sealed class UiHooks
{
    private static readonly log4net.ILog Log = AppLogger.For<UiHooks>();
    private readonly ScenarioContext scenarioContext;

    public UiHooks(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [BeforeScenario(Order = 0)]
    public void BeforeScenario()
    {
        _ = Configuration.BrowserType; // force configuration + logging setup
        EnsureLogDirectory();
        Log.Info($"Starting scenario: {scenarioContext.ScenarioInfo.Title}");

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

    [AfterScenario]
    public void AfterScenario()
    {
        try
        {
            if (scenarioContext.TestError != null)
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
            Log.Info($"Finished scenario: {scenarioContext.ScenarioInfo.Title} - {scenarioContext.ScenarioExecutionStatus}");
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
        var safeScenarioName = Regex.Replace(TestContext.CurrentContext.Test.Name, "[^a-zA-Z0-9_-]", "_");

        var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", "screenshots");
        Directory.CreateDirectory(screenshotsDir);

        var screenshotPath = Path.Combine(screenshotsDir, $"{safeScenarioName}_{timestamp}.png");
        try
        {
            screenshotDriver.GetScreenshot().SaveAsFile(screenshotPath);
            TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
            return screenshotPath;
        }
        catch (WebDriverException)
        {
            Log.Warn("Failed to capture screenshot because the browser session is unavailable.");
            return null;
        }
    }

    private static void EnsureLogDirectory()
    {
        var logPath = Configuration.Logging.FilePath;
        var directory = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}

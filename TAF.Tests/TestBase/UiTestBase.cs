using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;

namespace TAF.Tests.TestBase;

public abstract class UiTestBase
{
    [SetUp]
    public void SetUp()
    {
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
                SaveFailureScreenshot();
            }
        }
        finally
        {
            WebDriverWrapper.Quit();
        }
    }

    private static void SaveFailureScreenshot()
    {
        ITakesScreenshot? screenshotDriver;
        try
        {
            screenshotDriver = WebDriverWrapper.Driver as ITakesScreenshot;
        }
        catch (InvalidOperationException)
        {
            return;
        }

        if (screenshotDriver == null)
        {
            return;
        }

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
        var safeTestName = Regex.Replace(TestContext.CurrentContext.Test.Name, "[^a-zA-Z0-9_-]", "_");

        var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", "screenshots");
        Directory.CreateDirectory(screenshotsDir);

        var screenshotPath = Path.Combine(screenshotsDir, $"{safeTestName}_{timestamp}.png");
        screenshotDriver.GetScreenshot().SaveAsFile(screenshotPath);
        TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
    }
}

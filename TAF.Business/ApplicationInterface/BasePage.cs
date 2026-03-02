using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;

namespace TAF.Business.ApplicationInterface;

public abstract class BasePage
{
    protected IWebDriver Driver => WebDriverWrapper.Driver;

    protected void NavigateTo(string relativePath)
    {
        Driver.Navigate().GoToUrl(BuildAbsoluteUrl(relativePath));
    }

    protected static string BuildAbsoluteUrl(string relativePath)
    {
        var baseUrl = Configuration.AppUrl.TrimEnd('/');
        var path = relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
        return $"{baseUrl}{path}";
    }

    protected DefaultWait<IWebDriver> CreateWait(int? timeoutSeconds = null)
    {
        var wait = new DefaultWait<IWebDriver>(Driver)
        {
            Timeout = TimeSpan.FromSeconds(timeoutSeconds ?? Configuration.Timeouts.Default),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        return wait;
    }

    protected void ScrollIntoView(IWebElement element)
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
    }

    protected void AcceptCookiesIfVisible()
    {
        var cookieButtons = Driver.FindElements(BusinessData.CookieAcceptAllButton);
        if (cookieButtons.Count == 0)
            return;

        try
        {
            cookieButtons[0].Click();
        }
        catch (WebDriverException)
        {
            // Continue even if cookie button cannot be clicked.
        }
    }
}

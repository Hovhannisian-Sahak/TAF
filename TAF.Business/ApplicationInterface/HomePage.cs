using OpenQA.Selenium;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;
using TAF.Core.WebElementFamily;

namespace TAF.Business.ApplicationInterface;

public class HomePage
{
    private readonly Link careersNavigationLink = new(BusinessData.CareersNavigationLink);
    private readonly Link insightsNavigationLink = new(BusinessData.InsightsNavigationLink);

    private IWebDriver Driver => WebDriverWrapper.Driver;

    public void Open()
    {
        Driver.Navigate().GoToUrl(BuildAbsoluteUrl(BusinessData.HomeRelativeUrl));
    }

    public void OpenCareers()
    {
        TryOpenFromMenuOrNavigateDirect(careersNavigationLink, BusinessData.CareersRelativeUrl);
    }

    public void OpenInsights()
    {
        TryOpenFromMenuOrNavigateDirect(insightsNavigationLink, BusinessData.InsightsRelativeUrl);
    }

    public bool IsOpened()
    {
        return Driver.FindElements(BusinessData.HomeRoot).Count > 0 && !string.IsNullOrWhiteSpace(Driver.Url);
    }

    private static string BuildAbsoluteUrl(string relativePath)
    {
        var baseUrl = Configuration.AppUrl.TrimEnd('/');
        var path = relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
        return $"{baseUrl}{path}";
    }

    private void TryOpenFromMenuOrNavigateDirect(Link menuLink, string expectedPath)
    {
        try
        {
            menuLink.ClickAndWaitForUrl(expectedPath);
        }
        catch (WebDriverException)
        {
            Driver.Navigate().GoToUrl(BuildAbsoluteUrl(expectedPath));
        }
    }
}



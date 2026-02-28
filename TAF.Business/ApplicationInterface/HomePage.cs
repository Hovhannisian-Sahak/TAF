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
        careersNavigationLink.ClickAndWaitForUrl(BusinessData.CareersRelativeUrl);
    }

    public void OpenInsights()
    {
        insightsNavigationLink.ClickAndWaitForUrl(BusinessData.InsightsRelativeUrl);
    }

    public bool IsOpened()
    {
        return Driver.FindElements(BusinessData.HomeRoot).Count > 0 &&
               Driver.Url.StartsWith(Configuration.AppUrl, StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildAbsoluteUrl(string relativePath)
    {
        var baseUrl = Configuration.AppUrl.TrimEnd('/');
        var path = relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
        return $"{baseUrl}{path}";
    }
}



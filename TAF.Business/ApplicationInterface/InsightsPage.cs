using OpenQA.Selenium;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;

namespace TAF.Business.ApplicationInterface;

public class InsightsPage
{
    private IWebDriver Driver => WebDriverWrapper.Driver;

    public void Open()
    {
        Driver.Navigate().GoToUrl(BuildAbsoluteUrl(BusinessData.InsightsRelativeUrl));
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.InsightsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.InsightsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.InsightsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    private static string BuildAbsoluteUrl(string relativePath)
    {
        var baseUrl = Configuration.AppUrl.TrimEnd('/');
        var path = relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
        return $"{baseUrl}{path}";
    }
}



using OpenQA.Selenium;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;
using TAF.Core.WebDriver;

namespace TAF.Business.ApplicationInterface;

public class CareersPage
{
    private IWebDriver Driver => WebDriverWrapper.Driver;

    public void Open()
    {
        Driver.Navigate().GoToUrl(BuildAbsoluteUrl(BusinessData.CareersRelativeUrl));
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.CareersRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.CareersHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.CareersTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    private static string BuildAbsoluteUrl(string relativePath)
    {
        var baseUrl = Configuration.AppUrl.TrimEnd('/');
        var path = relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
        return $"{baseUrl}{path}";
    }
}



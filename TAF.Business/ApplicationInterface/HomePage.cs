using OpenQA.Selenium;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.WebElementFamily;

namespace TAF.Business.ApplicationInterface;

public class HomePage : BasePage
{
    private readonly Link careersNavigationLink = new(BusinessData.CareersNavigationLink);
    private readonly Link insightsNavigationLink = new(BusinessData.InsightsNavigationLink);

    public void Open()
    {
        NavigateTo(BusinessData.HomeRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public void OpenCareers()
    {
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(careersNavigationLink, BusinessData.CareersRelativeUrl);
    }

    public void OpenInsights()
    {
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(insightsNavigationLink, BusinessData.InsightsRelativeUrl);
    }

    public bool IsOpened()
    {
        return Driver.FindElements(BusinessData.HomeRoot).Count > 0 && !string.IsNullOrWhiteSpace(Driver.Url);
    }

    private void TryOpenFromMenuOrNavigateDirect(Link menuLink, string expectedPath)
    {
        try
        {
            menuLink.ClickAndWaitForUrl(expectedPath);
        }
        catch (WebDriverException)
        {
            NavigateTo(expectedPath);
        }
    }
}



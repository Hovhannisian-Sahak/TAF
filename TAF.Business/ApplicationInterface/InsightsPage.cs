using OpenQA.Selenium;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;

namespace TAF.Business.ApplicationInterface;

public class InsightsPage : BasePage
{
    public void Open()
    {
        NavigateTo(BusinessData.InsightsRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.InsightsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.InsightsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.InsightsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

}



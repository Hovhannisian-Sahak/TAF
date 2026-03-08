using System.Linq;
using OpenQA.Selenium.DevTools.V143.Page;
using TAF.Core.Configuration;
using TAF.Core.WebElementFamily;
using BusinessData = TAF.Business.Data.Data;
namespace TAF.Business.ApplicationInterface;

public class QuarterlyEarnings: BasePage
{
    public void Open()
    {
        NavigateTo(BusinessData.QuarterlyEarningsRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.QuarterlyEarningsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.QuarterlyEarningsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.QuarterlyEarningsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }
    
    public void ClickDownload()
    {
        var originalHandle = Driver.CurrentWindowHandle;
        var handlesBefore = Driver.WindowHandles;

        new Link(BusinessData.QuarterlyEarningsDownloadLink).ScrollToElementAndClick();

        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(_ => Driver.WindowHandles.Count > handlesBefore.Count);

        var newHandle = Driver.WindowHandles.First(h => !handlesBefore.Contains(h));
        Driver.SwitchTo().Window(newHandle);

        wait.Until(_ => Driver.Url.Contains(BusinessData.QuarterlyEarningsDownloadUrlContains, StringComparison.OrdinalIgnoreCase));
     
    }
    
}

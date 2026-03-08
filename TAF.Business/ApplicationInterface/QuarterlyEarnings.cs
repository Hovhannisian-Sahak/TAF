using System.Linq;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Core.WebElementFamily;
using BusinessData = TAF.Business.Data.Data;
namespace TAF.Business.ApplicationInterface;

public class QuarterlyEarnings: BasePage
{
    private static readonly log4net.ILog Log = AppLogger.For<QuarterlyEarnings>();

    public void Open()
    {
        Log.Info("Open Quarterly Earnings page.");
        NavigateTo(BusinessData.QuarterlyEarningsRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        Log.Info("Check Quarterly Earnings page opened.");
        return Driver.Url.Contains(BusinessData.QuarterlyEarningsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.QuarterlyEarningsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.QuarterlyEarningsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }
    
    public void ClickDownload()
    {
        Log.Info("Click Quarterly Earnings download link.");
        var originalHandle = Driver.CurrentWindowHandle;
        var handlesBefore = Driver.WindowHandles;

        new Link(BusinessData.QuarterlyEarningsDownloadLink).ScrollToElementAndClick();

        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(_ => Driver.WindowHandles.Count > handlesBefore.Count);

        var newHandle = Driver.WindowHandles.First(h => !handlesBefore.Contains(h));
        Driver.SwitchTo().Window(newHandle);

        wait.Until(_ => Driver.Url.Contains(BusinessData.QuarterlyEarningsDownloadUrlContains, StringComparison.OrdinalIgnoreCase));
        Log.Info($"Download page opened: {Driver.Url}");
    }
    
}

using TAF.Business.ApplicationInterface;
using TAF.Core.Logging;

namespace TAF.Business.Business;

public class QuarterlyEarningsContext
{
    private static readonly log4net.ILog Log = AppLogger.For<QuarterlyEarningsContext>();
    private readonly QuarterlyEarnings page = new();
    
    public QuarterlyEarningsContext OpenQuarterlyEarnings()
    {
        Log.Info("Open Quarterly Earnings page.");
        page.Open();
        return this;
    }
    
    public bool IsOpened()
    {
        return page.IsOpened();
    }
    
    public void ValidateOpened()
    {
        Log.Info("Validate Quarterly Earnings page opened.");
        if (!IsOpened())
        {
            throw new Exception("Quarterly Earnings page was not opened.");
        }
    }
    
        public QuarterlyEarningsContext ClickDownload()
        {
            Log.Info("Click Quarterly Earnings download link.");
            page.ClickDownload();
            return this;
        }
}

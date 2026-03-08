using TAF.Business.ApplicationInterface;

namespace TAF.Business.Business;

public class QuarterlyEarningsContext
{
    private readonly QuarterlyEarnings page = new();
    
    public QuarterlyEarningsContext OpenQuarterlyEarnings()
    {
        page.Open();
        return this;
    }
    
    public bool IsOpened()
    {
        return page.IsOpened();
    }
    
    public void ValidateOpened()
    {
        if (!IsOpened())
        {
            throw new Exception("Quarterly Earnings page was not opened.");
        }
    }
    
    public QuarterlyEarningsContext ClickDownload()
    {
        page.ClickDownload();
        return this;
    }
}
using NUnit.Framework;
using TAF.Business.Business;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;
/// <summary>
/// Quarterly Earnings page has a link to download.
/// </summary>
public class QuarterlyEarningsTests:UiTestBase
{
    [Test]
    public void Open_Download_Doc_In_New_Window()
    {
        new HomeContext()
            .OpenHome()
            .OpenQuarterlyEarnings()
            .ClickDownload();
    }
}
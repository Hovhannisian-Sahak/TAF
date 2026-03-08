using NUnit.Framework;
using TAF.Business.Business;
using TAF.Core.Logging;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;
/// <summary>
/// Quarterly Earnings page has a link to download.
/// </summary>
public class QuarterlyEarningsTests:UiTestBase
{
    private static readonly log4net.ILog Log = AppLogger.For<QuarterlyEarningsTests>();

    [Test]
    public void Open_Download_Doc_In_New_Window()
    {
        Log.Info("Test start: open Quarterly Earnings download link.");
        new HomeContext()
            .OpenHome()
            .OpenQuarterlyEarnings()
            .ClickDownload();
    }
}

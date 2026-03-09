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
        var quarterlyEarnings = new HomeContext()
            .OpenHome()
            .OpenQuarterlyEarnings();

        Assert.That(quarterlyEarnings.IsOpened(), Is.True, "Expected Quarterly Earnings page to be opened.");

        quarterlyEarnings.ClickDownload();
        Assert.That(quarterlyEarnings.IsDownloadPageOpened(), Is.True,
            "Expected Quarterly Earnings download page to be opened in a new window.");
    }
}

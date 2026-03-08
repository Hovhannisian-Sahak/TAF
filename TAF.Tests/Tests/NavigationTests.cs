using NUnit.Framework;
using TAF.Business.Business;
using TAF.Core.Logging;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

[TestFixture]
public class NavigationTests : UiTestBase
{
    private static readonly log4net.ILog Log = AppLogger.For<NavigationTests>();

    [Test]
    public void HomePage_Should_Open()
    {
        Log.Info("Test start: open Home page.");
        new HomeContext()
            .OpenHome()
            .ValidateOpened();
    }

    [Test]
    public void CareersPage_Should_Open_From_Home()
    {
        Log.Info("Test start: open Careers from Home.");
        new HomeContext()
            .OpenHome()
            .OpenCareers()
            .ValidateOpened();
    }

    [Test]
    public void InsightsPage_Should_Open_From_Home()
    {
        Log.Info("Test start: open Insights from Home.");
        new HomeContext()
            .OpenHome()
            .OpenInsights()
            .ValidateOpened();
    }
    
    [Test]
    public void QuarterlyResultsPage_Should_Open_From_Home()
    {
        Log.Info("Test start: open Quarterly Earnings from Home.");
        new HomeContext()
            .OpenHome()
            .OpenQuarterlyEarnings()
            .ValidateOpened();
    }
}

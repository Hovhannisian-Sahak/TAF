using NUnit.Framework;
using TAF.Business.Business;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

[TestFixture]
public class NavigationTests : UiTestBase
{
    [Test]
    public void HomePage_Should_Open()
    {
        new HomeContext()
            .OpenHome()
            .ValidateOpened();
    }

    [Test]
    public void CareersPage_Should_Open_From_Home()
    {
        new HomeContext()
            .OpenHome()
            .OpenCareers()
            .ValidateOpened();
    }

    [Test]
    public void InsightsPage_Should_Open_From_Home()
    {
        new HomeContext()
            .OpenHome()
            .OpenInsights()
            .ValidateOpened();
    }
}

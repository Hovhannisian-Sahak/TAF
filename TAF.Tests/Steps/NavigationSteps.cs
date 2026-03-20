using NUnit.Framework;
using Reqnroll;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class NavigationSteps : StepBase
{
    public NavigationSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [Given("I am on the Home page")]
    public void GivenIAmOnTheHomePage()
    {
        GetHome().OpenHome();
    }

    [When("I open the \"(.*)\" page from Home")]
    public void WhenIOpenThePageFromHome(string pageName)
    {
        var home = GetHome();
        switch (NormalizePageName(pageName))
        {
            case "careers":
                ScenarioContext[ScenarioKeys.CareersContext] = home.OpenCareers();
                return;
            case "insights":
                ScenarioContext[ScenarioKeys.InsightsContext] = home.OpenInsights();
                return;
            case "quarterly earnings":
                ScenarioContext[ScenarioKeys.QuarterlyEarningsContext] = home.OpenQuarterlyEarnings();
                return;
            default:
                Assert.Fail($"Unsupported page name: '{pageName}'.");
                return;
        }
    }

    [When("I open the Insights page from Home")]
    public void WhenIOpenInsightsFromHome()
    {
        var home = GetHome();
        ScenarioContext[ScenarioKeys.InsightsContext] = home.OpenInsights();
    }

    [When("I open the Quarterly Earnings page from Home")]
    public void WhenIOpenQuarterlyEarningsFromHome()
    {
        var home = GetHome();
        ScenarioContext[ScenarioKeys.QuarterlyEarningsContext] = home.OpenQuarterlyEarnings();
    }

    [Then("the \"(.*)\" page should be opened")]
    public void ThenThePageShouldBeOpened(string pageName)
    {
        switch (NormalizePageName(pageName))
        {
            case "careers":
                Assert.That(GetCareers().IsOpened(), Is.True, "Expected Careers page to be opened.");
                return;
            case "insights":
                Assert.That(GetInsights().IsOpened(), Is.True, "Expected Insights page to be opened.");
                return;
            case "quarterly earnings":
                Assert.That(GetQuarterlyEarnings().IsOpened(), Is.True, "Expected Quarterly Earnings page to be opened.");
                return;
            default:
                Assert.Fail($"Unsupported page name: '{pageName}'.");
                return;
        }
    }

    [Then("the Quarterly Earnings page should be opened")]
    public void ThenTheQuarterlyEarningsPageShouldBeOpened()
    {
        Assert.That(GetQuarterlyEarnings().IsOpened(), Is.True, "Expected Quarterly Earnings page to be opened.");
    }

    private static string NormalizePageName(string pageName)
    {
        return pageName.Trim().ToLowerInvariant();
    }
}

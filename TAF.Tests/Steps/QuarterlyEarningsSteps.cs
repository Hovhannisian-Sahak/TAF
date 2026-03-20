using NUnit.Framework;
using Reqnroll;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class QuarterlyEarningsSteps : StepBase
{
    public QuarterlyEarningsSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [When("I open the Quarterly Earnings download link")]
    public void WhenIOpenTheQuarterlyEarningsDownloadLink()
    {
        var quarterly = GetQuarterlyEarnings();
        quarterly.ClickDownload();
    }

    [Then("the Quarterly Earnings download page should be opened")]
    public void ThenTheQuarterlyEarningsDownloadPageShouldBeOpened()
    {
        var quarterly = GetQuarterlyEarnings();
        Assert.That(quarterly.IsDownloadPageOpened(), Is.True,
            "Expected Quarterly Earnings download page to be opened.");
    }

}

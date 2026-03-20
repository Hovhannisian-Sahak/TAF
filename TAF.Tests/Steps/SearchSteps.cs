using NUnit.Framework;
using Reqnroll;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class SearchSteps : StepBase
{
    public SearchSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [When("I search globally for \"(.*)\"")]
    public void WhenISearchGloballyFor(string term)
    {
        var home = GetHome();
        home.Search(term);
    }

    [Then("search results should contain \"(.*)\"")]
    public void ThenSearchResultsShouldContain(string term)
    {
        var home = GetHome();
        Assert.That(home.AreSearchResultsContain(term), Is.True,
            $"Expected search results to contain '{term}'.");
    }

}

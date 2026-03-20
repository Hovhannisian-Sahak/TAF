using NUnit.Framework;
using Reqnroll;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class ServicesSteps : StepBase
{
    public ServicesSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [When("I open the \"(.*)\" service from Home")]
    public void WhenIOpenTheServiceFromHome(string categoryName)
    {
        var home = GetHome();
        ScenarioContext[ScenarioKeys.ServicesContext] = home.OpenServiceCategory(categoryName);
    }

    [Then("the \"(.*)\" service page title should be displayed")]
    public void ThenTheServicePageTitleShouldBeDisplayed(string categoryName)
    {
        var services = GetServices();
        Assert.That(services.IsTitleDisplayed(categoryName), Is.True,
            $"Expected Services page title to contain '{categoryName}'.");
    }

    [Then("the \"Our Related Expertise\" section should be displayed")]
    public void ThenTheRelatedExpertiseSectionShouldBeDisplayed()
    {
        var services = GetServices();
        Assert.That(services.IsRelatedExpertiseDisplayed(), Is.True,
            "Expected 'Our Related Expertise' section to be displayed.");
    }
}

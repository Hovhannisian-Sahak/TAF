using NUnit.Framework;
using Reqnroll;
using TAF.Business.Business;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class CareersSteps : StepBase
{
    public CareersSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [When("I search careers for \"(.*)\" in \"(.*)\"")]
    public void WhenISearchCareersForIn(string keyword, string location)
    {
        var careers = GetOrOpenCareers();
        careers.SearchByCriteria(keyword, location);
    }

    [When("I open the latest result and view apply")]
    public void WhenIOpenTheLatestResultAndViewApply()
    {
        var careers = GetOrOpenCareers();
        careers.OpenLatestResultAndViewApply();
    }

    [Then("the vacancy details should contain keyword \"(.*)\"")]
    public void ThenTheVacancyDetailsShouldContainKeyword(string keyword)
    {
        var careers = GetOrOpenCareers();
        Assert.That(careers.IsKeywordPresentOnDetails(keyword), Is.True,
            $"Expected keyword '{keyword}' to be present on vacancy details page.");
    }

    [Then("the vacancy details should contain location \"(.*)\"")]
    public void ThenTheVacancyDetailsShouldContainLocation(string location)
    {
        var careers = GetOrOpenCareers();
        Assert.That(careers.IsCountryNamePresentOnDetails(location), Is.True,
            $"Expected location '{location}' to be present on vacancy details page.");
    }

    private CareersContext GetOrOpenCareers()
    {
        if (ScenarioContext.TryGetValue(ScenarioKeys.CareersContext, out CareersContext? careers) && careers != null)
        {
            return careers;
        }

        var home = GetHome();
        careers = home.OpenCareers();
        ScenarioContext[ScenarioKeys.CareersContext] = careers;
        return careers;
    }
}
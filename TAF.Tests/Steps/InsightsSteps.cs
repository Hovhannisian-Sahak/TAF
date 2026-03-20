using NUnit.Framework;
using Reqnroll;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Steps;

[Binding]
public sealed class InsightsSteps : StepBase
{
    public InsightsSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [When("I swipe the Insights carousel")]
    public void WhenISwipeTheInsightsCarousel()
    {
        var insights = GetInsights();
        insights.SwipeCarousel();
        ScenarioContext[ScenarioKeys.CarouselTitle] = insights.GetCarouselSlideTitle();
    }

    [When("I open the current Insights article")]
    public void WhenIOpenTheCurrentInsightsArticle()
    {
        var insights = GetInsights();
        insights.OpenArticle();
    }

    [Then("the opened article title should match the carousel title")]
    public void ThenTheOpenedArticleTitleShouldMatchTheCarouselTitle()
    {
        var insights = GetInsights();
        var expectedTitle = ScenarioContext[ScenarioKeys.CarouselTitle] as string ?? string.Empty;
        var actualTitle = insights.GetOpenedArticleTitle();
        Assert.That(actualTitle, Is.EqualTo(expectedTitle).IgnoreCase,
            "Expected opened article title to match the carousel title.");
    }
}

using NUnit.Framework;
using TAF.Business.Business;
using TAF.Core.Logging;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

public class InsightsTests : UiTestBase
{
    private static readonly log4net.ILog Log = AppLogger.For<InsightsTests>();

    [Test]
    public void Swipe_Carousel_On_Insights_Page()
    {
        Log.Info("Test start: swipe insights carousel and open article.");
        var insights = new HomeContext()
            .OpenHome()
            .OpenInsights()
            .SwipeCarousel();
        var firstArticleTitle = insights.GetCarouselSlideTitle();
        Assert.That(firstArticleTitle, Is.Not.Null.And.Not.Empty, "Expected carousel article title to be populated.");

        insights.OpenArticle();
        Assert.That(insights.GetOpenedArticleTitle(), Is.EqualTo(firstArticleTitle).IgnoreCase,
            "Expected opened article title to match the carousel title.");
    }
}

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
        new HomeContext()
            .OpenHome()
            .OpenInsights()
            .SwipeCarousel();
        var firstArticleTitle = new InsightsContext().GetCarouselSlideTitle();
        new InsightsContext()
            .OpenArticle()
            .ValidateOpenedArticleTitle(firstArticleTitle);
    }
}

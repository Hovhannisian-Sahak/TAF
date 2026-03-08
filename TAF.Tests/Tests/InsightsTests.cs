using NUnit.Framework;
using TAF.Business.Business;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

public class InsightsTests : UiTestBase
{
    [Test]
    public void Swipe_Carousel_On_Insights_Page()
    {
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

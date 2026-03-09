using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using TAF.Core.Logging;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class InsightsContext
{
    private static readonly log4net.ILog Log = AppLogger.For<InsightsContext>();
    private readonly InsightsPage page = new();

    public InsightsContext OpenInsights()
    {
        Log.Info("Open Insights page.");
        page.Open();
        return this;
    }

    public bool IsOpened()
    {
        return page.IsOpened();
    }

    public void ValidateOpened()
    {
        Log.Info("Validate Insights page opened.");
        ValidationRules.ValidatePageState(IsOpened(), "Insights page was not opened.");
    }

    public InsightsContext SwipeCarousel()
    {
        Log.Info("Swipe Insights carousel.");
        page.SwipeCarousel();
        return this;
    }

    public string GetCarouselSlideTitle()
    {
        Log.Info("Get current Insights carousel slide title.");
        return page.GetCurrentArticleTitle();
    }
    
    public InsightsContext OpenArticle()
    {
        Log.Info("Open current Insights article.");
        page.OpenArticle();
        return this;
    }
    
    public InsightsContext ValidateOpenedArticleTitle(string expectedTitle)
    {
        Log.Info($"Validate opened article title matches: '{expectedTitle}'.");
        page.ValidateOpenedArticleTitle(expectedTitle);
        return this;
    }

    public string GetOpenedArticleTitle()
    {
        Log.Info("Get opened article title.");
        return page.GetOpenedArticleTitle();
    }
}

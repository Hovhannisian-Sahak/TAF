using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Linq;
using TAF.Business.Data;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Core.WebElementFamily;
using BusinessData = TAF.Business.Data.Data;

namespace TAF.Business.ApplicationInterface;

public class InsightsPage : BasePage
{
    private static readonly log4net.ILog Log = AppLogger.For<InsightsPage>();

    public void Open()
    {
        Log.Info("Open Insights page.");
        NavigateTo(BusinessData.InsightsRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        Log.Info("Check Insights page opened.");
        return Driver.Url.Contains(BusinessData.InsightsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.InsightsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.InsightsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    public void SwipeCarousel()
    {
        Log.Info("Swipe Insights carousel.");
        new Button(BusinessData.InsightsCarouselNextButton).ClickWithActions();
    }
    
    public string GetCurrentArticleTitle()
    {
        Log.Info("Get current Insights carousel title.");
        return new TextElement(BusinessData.CarouselSlideArticelTitle).Text;
    }
    public void OpenArticle()
    {
        Log.Info("Open Insights article from carousel.");
        var link = new Link(BusinessData.InsightsReadMoreLink);
        var href = link.Href;

        if (!string.IsNullOrWhiteSpace(href))
        {
            Log.Info($"Navigate directly to article: {href}");
            Driver.Navigate().GoToUrl(href);
            return;
        }

        link.ScrollToElementAndClick();
    }
    public void ValidateOpenedArticleTitle(string expectedTitle)
    {
        var title = new TextElement(BusinessData.CarouselSlideArticleHeader).Text;
        Log.Info($"Validate opened article title. Expected: '{expectedTitle}', Actual: '{title}'.");
        if (!title.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Opened article title '{title}' does not match expected title '{expectedTitle}'.");
        }
    }
}

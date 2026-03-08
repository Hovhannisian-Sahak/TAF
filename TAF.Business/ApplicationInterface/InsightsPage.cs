using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Linq;
using TAF.Business.Data;
using TAF.Core.Configuration;
using TAF.Core.WebElementFamily;
using BusinessData = TAF.Business.Data.Data;

namespace TAF.Business.ApplicationInterface;

public class InsightsPage : BasePage
{
    public void Open()
    {
        NavigateTo(BusinessData.InsightsRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.InsightsRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.InsightsHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.InsightsTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    public void SwipeCarousel()
    {
        new Button(BusinessData.InsightsCarouselNextButton).ClickWithActions();
    }
    
    public string GetCurrentArticleTitle()
    {
        return new TextElement(BusinessData.CarouselSlideArticelTitle).Text;
    }
    public void OpenArticle()
    {
        var link = new Link(BusinessData.InsightsReadMoreLink);
        var href = link.Href;

        if (!string.IsNullOrWhiteSpace(href))
        {
            Driver.Navigate().GoToUrl(href);
            return;
        }

        link.ScrollToElementAndClick();
    }
    public void ValidateOpenedArticleTitle(string expectedTitle)
    {
        var title = new TextElement(BusinessData.CarouselSlideArticleHeader).Text;
        Console.WriteLine(expectedTitle);
        Console.WriteLine(title);
        if (!title.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Opened article title '{title}' does not match expected title '{expectedTitle}'.");
        }
    }
}

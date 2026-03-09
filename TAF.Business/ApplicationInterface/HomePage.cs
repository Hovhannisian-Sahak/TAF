using OpenQA.Selenium;
using System.Linq;
using TAF.Business.Data;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.WebElementFamily;

namespace TAF.Business.ApplicationInterface;

public class HomePage : BasePage
{
    private static readonly log4net.ILog Log = AppLogger.For<HomePage>();
    private readonly Link careersNavigationLink = new(BusinessData.CareersNavigationLink);
    private readonly Link insightsNavigationLink = new(BusinessData.InsightsNavigationLink);
    private readonly Link quarterlyEarningsNavigationLink = new(BusinessData.QuarterlyEarningsNavigationLink);
    public void Open()
    {
        Log.Info("Open Home page.");
        NavigateTo(BusinessData.HomeRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public void OpenCareers()
    {
        Log.Info("Open Careers from Home.");
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(careersNavigationLink, BusinessData.CareersRelativeUrl);
    }

    public void OpenInsights()
    {
        Log.Info("Open Insights from Home.");
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(insightsNavigationLink, BusinessData.InsightsRelativeUrl);
    }
    
    public void OpenQuarterlyEarnings()
    {
        Log.Info("Open Quarterly Earnings from Home.");
        AcceptCookiesIfVisible();
        MoveToAboutLink();
        TryOpenFromMenuOrNavigateDirect(quarterlyEarningsNavigationLink,BusinessData.QuarterlyEarningsRelativeUrl);
    }

    public bool IsOpened()
    {
        Log.Info("Check Home page opened.");
        return Driver.FindElements(BusinessData.HomeRoot).Count > 0 && !string.IsNullOrWhiteSpace(Driver.Url);
    }
    
    private void TryOpenFromMenuOrNavigateDirect(Link menuLink, string expectedPath)
    {
        try
        {
            menuLink.ClickAndWaitForUrl(expectedPath);
        }
        catch (WebDriverException)
        {
            NavigateTo(expectedPath);
        }
    }
    
    public void Search(string searchTerm)
    {
        Log.Info($"Global search: '{searchTerm}'.");
        ClickSearchIcon();
        EnterSearchTerm(searchTerm);
        ClickSearchButton();
    }
    
    public void ClickSearchIcon()
    {
        var button = new Button(BusinessData.GlobalSearchIcon);
        button.Click();
    }
    
    public void EnterSearchTerm(string searchTerm)
    {
        var input = new TextBox(BusinessData.GlobalSearchInput);
        input.EnterText(searchTerm);
        
    }
    
    public void ClickSearchButton()
    {
        var button = new Button(BusinessData.GlobalSearchButton);
        button.Click();
    }
    
    public void ValidateSearchResultsContain(string expectedText)
    {
        Log.Info($"Validate search results contain: '{expectedText}'.");
        AreSearchResultsContain(expectedText);
    }

    public bool AreSearchResultsContain(string expectedText)
    {
        Log.Info($"Check search results contain: '{expectedText}'.");
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
        {
            var links = driver.FindElements(BusinessData.SearchResultTexts);
            return links.Count > 0 &&
                   links.All(link => link.Text.Contains(expectedText, StringComparison.OrdinalIgnoreCase));
        });
    }
    
    private void MoveToAboutLink()
    {
        var aboutLink = new Link(BusinessData.AboutUsNavigationLink);
        aboutLink.HoverToElement();

        // if (!IsQuarterlyEarningsVisible())
        // {
        //     aboutLink.ClickWithActions();
        // }

        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(_ => IsQuarterlyEarningsVisible());
    }

    private bool IsQuarterlyEarningsVisible()
    {
        var links = Driver.FindElements(BusinessData.QuarterlyEarningsNavigationLink);
        return links.Any(link => link.Displayed);
    }
}

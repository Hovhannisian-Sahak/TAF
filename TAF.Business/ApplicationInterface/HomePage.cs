using OpenQA.Selenium;
using System.Linq;
using TAF.Business.Data;
using TAF.Core.Configuration;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.WebElementFamily;

namespace TAF.Business.ApplicationInterface;

public class HomePage : BasePage
{
    private readonly Link careersNavigationLink = new(BusinessData.CareersNavigationLink);
    private readonly Link insightsNavigationLink = new(BusinessData.InsightsNavigationLink);
    private readonly Link quarterlyEarningsNavigationLink = new(BusinessData.QuarterlyEarningsNavigationLink);
    public void Open()
    {
        NavigateTo(BusinessData.HomeRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public void OpenCareers()
    {
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(careersNavigationLink, BusinessData.CareersRelativeUrl);
    }

    public void OpenInsights()
    {
        AcceptCookiesIfVisible();
        TryOpenFromMenuOrNavigateDirect(insightsNavigationLink, BusinessData.InsightsRelativeUrl);
    }
    
    public void OpenQuarterlyEarnings()
    {
        AcceptCookiesIfVisible();
        MoveToAboutLink();
        TryOpenFromMenuOrNavigateDirect(quarterlyEarningsNavigationLink,BusinessData.QuarterlyEarningsRelativeUrl);
    }

    public bool IsOpened()
    {
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
        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(driver =>
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

using OpenQA.Selenium;
using System.Linq;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Core.WebElementFamily;

namespace TAF.Business.ApplicationInterface;

public class CareersPage : BasePage
{
    private static readonly log4net.ILog Log = AppLogger.For<CareersPage>();

    public void Open()
    {
        Log.Info("Open Careers page.");
        NavigateTo(BusinessData.CareersRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        Log.Info("Check Careers page opened.");
        return Driver.Url.Contains(BusinessData.CareersRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.CareersHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.CareersTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    public void Search(string keyword, string location)
    {
        Log.Info($"Search careers. Keyword: '{keyword}', Location: '{location}'.");
        ClickStartSearch();
        EnterRole(keyword);
        EnterCountryName(location);
        SelectRemote();
        ClickFind();
    }
    
    private void ClickStartSearch()
    {
        var button = new Link(BusinessData.StartSearchButton);
        button.Click();
    }

    public void OpenLatestAndViewApply()
    {
        Log.Info("Open latest result and click apply.");
        AcceptCookiesIfVisible();
        WaitForSearchResults();

        var wait = CreateWait(Configuration.Timeouts.Long);
        var expandButton = wait.Until(driver =>
        {
            var elements = driver.FindElements(BusinessData.CardExpandButton);
            return elements.FirstOrDefault(e => e.Displayed && e.Enabled);
        });
        expandButton?.Click();

        var applyButton = wait.Until(driver =>
        {
            var elements = driver.FindElements(BusinessData.ApplyButton);
            return elements.FirstOrDefault(e => e.Displayed && e.Enabled);
        });
        applyButton?.Click();

        try
        {
            var closeWait = CreateWait(Configuration.Timeouts.Default);
            var closeButton = closeWait.Until(driver =>
            {
                var elements = driver.FindElements(BusinessData.CloseModalButton);
                return elements.FirstOrDefault(e => e.Displayed && e.Enabled);
            });
            closeButton?.Click();
        }
        catch (WebDriverTimeoutException)
        {
            // Modal did not appear; nothing to close.
        }
    }

    public bool IsKeywordPresentOnPage(string keyword)
    {
        Log.Info($"Check keyword present on page: '{keyword}'.");
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
            driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
    
    public bool IsCountryNamePresentOnPage(string location)
    {
        Log.Info($"Check location present on page: '{location}'.");
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
            driver.PageSource.Contains(location, StringComparison.OrdinalIgnoreCase));
    }

    private void EnterRole(string keyword)
    {
        var input = new TextBox(BusinessData.KeywordInput);
        input.EnterText(keyword);
    }

    private void EnterCountryName(string name)
    {
        var input = new TextBox(BusinessData.LocationInput);
        input.EnterText(name);
    }

    private void SelectRemote()
    {
        var option = new CheckBox(BusinessData.RemoteOption);
        if (option.IsChecked)
            return;
        
        option.Check();
    }

    private void ClickFind()
    {
        var button = new Button(BusinessData.FindButton);
        button.Click();
    }
    
    private void WaitForSearchResults()
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(driver =>
            driver.FindElements(BusinessData.SearchResultsCards).Count > 0);
    }

}

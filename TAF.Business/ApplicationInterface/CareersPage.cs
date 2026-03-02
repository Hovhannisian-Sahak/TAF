using OpenQA.Selenium;
using System.Linq;
using TAF.Business.Data;
using BusinessData = TAF.Business.Data.Data;
using TAF.Core.Configuration;

namespace TAF.Business.ApplicationInterface;

public class CareersPage : BasePage
{
    public void Open()
    {
        NavigateTo(BusinessData.CareersRelativeUrl);
        AcceptCookiesIfVisible();
    }

    public bool IsOpened()
    {
        return Driver.Url.Contains(BusinessData.CareersRelativeUrl, StringComparison.OrdinalIgnoreCase) &&
               (Driver.FindElements(BusinessData.CareersHeader).Count > 0 ||
                Driver.Title.Contains(BusinessData.CareersTitleKeyword, StringComparison.OrdinalIgnoreCase));
    }

    public void Search(string keyword, string location, bool remoteOnly)
    {
        AcceptCookiesIfVisible();
        OpenSearchPanelIfNeeded();
        EnterKeyword(keyword);
        SelectLocation(location);

        if (remoteOnly)
        {
            SelectRemote();
        }

        ClickFind();
    }

    public void OpenLatestAndViewApply()
    {
        AcceptCookiesIfVisible();
        WaitForSearchResults();

        var cards = Driver.FindElements(BusinessData.SearchResultsCards);
        if (cards.Count > 0)
        {
            var latestCard = cards[cards.Count - 1];
            var nestedButtons = latestCard.FindElements(By.XPath(
                ".//a[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'view and apply')] | " +
                ".//button[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'view and apply')]"));

            if (nestedButtons.Count > 0)
            {
                ScrollIntoView(nestedButtons[0]);
                nestedButtons[0].Click();
                return;
            }
        }

        var allButtons = Driver.FindElements(BusinessData.ViewAndApplyButtons);
        if (allButtons.Count == 0)
        {
            var resultLinks = Driver.FindElements(BusinessData.ResultTitleLinks);
            if (resultLinks.Count == 0)
                throw new InvalidOperationException("No result link or 'View and apply' button was found in search results.");

            var latestLink = resultLinks[resultLinks.Count - 1];
            ScrollIntoView(latestLink);
            latestLink.Click();
            return;
        }

        var latestButton = allButtons[allButtons.Count - 1];
        ScrollIntoView(latestButton);
        latestButton.Click();
    }

    public bool IsKeywordPresentOnPage(string keyword)
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
            driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    private void EnterKeyword(string keyword)
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        var input = wait.Until(driver => driver.FindElement(BusinessData.KeywordInput));
        ScrollIntoView(input);
        input.Clear();
        input.SendKeys(keyword);
    }

    private void OpenSearchPanelIfNeeded()
    {
        if (Driver.FindElements(BusinessData.KeywordInput).Count > 0)
            return;

        var searchTriggers = Driver.FindElements(BusinessData.StartSearchHereButton);
        if (searchTriggers.Count == 0)
            return;

        try
        {
            ScrollIntoView(searchTriggers[0]);
            searchTriggers[0].Click();
        }
        catch (WebDriverException)
        {
            // Ignore and let keyword wait handle final timeout if panel cannot be opened.
        }
    }

    private void SelectLocation(string location)
    {
        for (var attempt = 0; attempt < Configuration.Timeouts.Retry; attempt++)
        {
            try
            {
                var wait = CreateWait(Configuration.Timeouts.Long);
                var input = wait.Until(driver =>
                {
                    var candidates = driver.FindElements(BusinessData.LocationInput);
                    return candidates.FirstOrDefault(e => e.Displayed) ?? candidates.FirstOrDefault();
                });

                if (input == null)
                    throw new NoSuchElementException("Location input was not found.");

                ScrollIntoView(input);
                input.SendKeys(Keys.Control + "a");
                input.SendKeys(Keys.Delete);

                if (!location.Equals("All Locations", StringComparison.OrdinalIgnoreCase))
                {
                    input.SendKeys(location);
                    input.SendKeys(Keys.Enter);
                }

                return;
            }
            catch (StaleElementReferenceException) when (attempt < Configuration.Timeouts.Retry - 1)
            {
                // retry
            }
            catch (ElementNotInteractableException) when (attempt < Configuration.Timeouts.Retry - 1)
            {
                // retry
            }
        }

        throw new InvalidOperationException("Failed to set location filter.");
    }

    private void SelectRemote()
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        var option = wait.Until(driver => driver.FindElement(BusinessData.RemoteOption));
        ScrollIntoView(option);
        option.Click();
    }

    private void ClickFind()
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        var button = wait.Until(driver => driver.FindElement(BusinessData.FindButton));
        ScrollIntoView(button);
        button.Click();
    }

    private void WaitForSearchResults()
    {
        var wait = CreateWait(Configuration.Timeouts.Long);
        wait.Until(driver =>
            driver.FindElements(BusinessData.SearchResultsCards).Count > 0 ||
            driver.FindElements(BusinessData.ViewAndApplyButtons).Count > 0);
    }

}

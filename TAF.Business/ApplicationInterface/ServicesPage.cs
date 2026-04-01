using OpenQA.Selenium;
using System;
using System.Linq;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using BusinessData = TAF.Business.Data.Data;

namespace TAF.Business.ApplicationInterface;

public class ServicesPage : BasePage
{
    private static readonly log4net.ILog Log = AppLogger.For<ServicesPage>();

    public bool IsTitleDisplayed(string expectedTitle)
    {
        Log.Info($"Check Services page title contains: '{expectedTitle}'.");
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
        {
            var titles = driver.FindElements(BusinessData.ServicesPageTitle);
            return titles.Any(title =>
                title.Displayed &&
                title.Text.Contains(expectedTitle, StringComparison.OrdinalIgnoreCase));
        });
    }

    public bool IsRelatedExpertiseSectionDisplayed()
    {
        Log.Info("Check 'Our Related Expertise' section displayed.");
        var wait = CreateWait(Configuration.Timeouts.Long);
        return wait.Until(driver =>
        {
            var sections = driver.FindElements(BusinessData.RelatedExpertiseSectionHeader);
            var section = sections.FirstOrDefault(e => e.Displayed);
            if (section == null)
            {
                return false;
            }

            ScrollIntoView(section);
            return section.Displayed;
        });
    }
}

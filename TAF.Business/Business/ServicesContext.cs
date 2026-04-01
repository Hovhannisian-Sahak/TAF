using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using TAF.Core.Logging;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class ServicesContext
{
    private static readonly log4net.ILog Log = AppLogger.For<ServicesContext>();
    private readonly ServicesPage page = new();

    public bool IsTitleDisplayed(string expectedTitle)
    {
        Log.Info($"Check Services page title contains '{expectedTitle}'.");
        return page.IsTitleDisplayed(expectedTitle);
    }

    public void ValidateTitleDisplayed(string expectedTitle)
    {
        Log.Info($"Validate Services page title contains '{expectedTitle}'.");
        ValidationRules.ValidatePageState(
            page.IsTitleDisplayed(expectedTitle),
            $"Services page title does not contain '{expectedTitle}'.");
    }

    public bool IsRelatedExpertiseDisplayed()
    {
        Log.Info("Check 'Our Related Expertise' section displayed.");
        return page.IsRelatedExpertiseSectionDisplayed();
    }

    public void ValidateRelatedExpertiseDisplayed()
    {
        Log.Info("Validate 'Our Related Expertise' section displayed.");
        ValidationRules.ValidatePageState(
            page.IsRelatedExpertiseSectionDisplayed(),
            "Expected 'Our Related Expertise' section to be displayed.");
    }
}

using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using TAF.Core.Logging;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class CareersContext
{
    private static readonly log4net.ILog Log = AppLogger.For<CareersContext>();
    private readonly CareersPage page = new();

    public CareersContext OpenCareers()
    {
        Log.Info("Open Careers page.");
        page.Open();
        return this;
    }

    public bool IsOpened()
    {
        return page.IsOpened();
    }

    public void ValidateOpened()
    {
        Log.Info("Validate Careers page opened.");
        ValidationRules.ValidatePageState(IsOpened(), "Careers page was not opened.");
    }

    public CareersContext SearchByCriteria(string keyword, string location)
    {
        Log.Info($"Search careers. Keyword: '{keyword}', Location: '{location}'.");
        page.Search(keyword, location);
        return this;
    }

    public CareersContext OpenLatestResultAndViewApply()
    {
        Log.Info("Open latest search result and view apply.");
        page.OpenLatestAndViewApply();
        return this;
    }

    public CareersContext ValidateKeywordPresentOnDetails(string keyword)
    {
        Log.Info($"Validate keyword present on details: '{keyword}'.");
        ValidationRules.ValidatePageState(
            page.IsKeywordPresentOnPage(keyword),
            $"Keyword '{keyword}' was not found on vacancy details page.");
        return this;
    }
    public CareersContext ValidateCountryNamePresentOnDetails(string location)
    {
        Log.Info($"Validate location present on details: '{location}'.");
        ValidationRules.ValidatePageState(
            page.IsCountryNamePresentOnPage(location),
            $"Location '{location}' was not found on vacancy details page.");
        return this;
    }
}

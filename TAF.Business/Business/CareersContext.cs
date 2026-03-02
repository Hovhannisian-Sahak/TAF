using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class CareersContext
{
    private readonly CareersPage page = new();

    public CareersContext OpenCareers()
    {
        page.Open();
        return this;
    }

    public bool IsOpened()
    {
        return page.IsOpened();
    }

    public void ValidateOpened()
    {
        ValidationRules.ValidatePageState(IsOpened(), "Careers page was not opened.");
    }

    public CareersContext SearchByCriteria(string keyword, string location, bool remoteOnly = true)
    {
        page.Search(keyword, location, remoteOnly);
        return this;
    }

    public CareersContext OpenLatestResultAndViewApply()
    {
        page.OpenLatestAndViewApply();
        return this;
    }

    public CareersContext ValidateKeywordPresentOnDetails(string keyword)
    {
        ValidationRules.ValidatePageState(
            page.IsKeywordPresentOnPage(keyword),
            $"Keyword '{keyword}' was not found on vacancy details page.");
        return this;
    }
}

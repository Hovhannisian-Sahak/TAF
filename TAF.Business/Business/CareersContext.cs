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
}


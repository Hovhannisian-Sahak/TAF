using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class HomeContext
{
    private readonly HomePage page = new();

    public HomeContext OpenHome()
    {
        page.Open();
        return this;
    }

    public CareersContext OpenCareers()
    {
        page.OpenCareers();
        return new CareersContext();
    }

    public InsightsContext OpenInsights()
    {
        page.OpenInsights();
        return new InsightsContext();
    }

    public bool IsOpened()
    {
        return page.IsOpened();
    }

    public void ValidateOpened()
    {
        ValidationRules.ValidatePageState(IsOpened(), "Home page was not opened.");
    }
}


using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class InsightsContext
{
    private readonly InsightsPage page = new();

    public InsightsContext OpenInsights()
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
        ValidationRules.ValidatePageState(IsOpened(), "Insights page was not opened.");
    }
}


using TAF.Business.ApplicationInterface;
using TAF.Business.Validations;
using TAF.Core.Logging;
using ValidationRules = TAF.Business.Validations.Validations;

namespace TAF.Business.Business;

public class HomeContext
{
    private static readonly log4net.ILog Log = AppLogger.For<HomeContext>();
    private readonly HomePage page = new();

    public HomeContext OpenHome()
    {
        Log.Info("Open Home page.");
        page.Open();
        return this;
    }

    public CareersContext OpenCareers()
    {
        Log.Info("Navigate to Careers from Home.");
        page.OpenCareers();
        return new CareersContext();
    }

    public InsightsContext OpenInsights()
    {
        Log.Info("Navigate to Insights from Home.");
        page.OpenInsights();
        return new InsightsContext();
    }

    public QuarterlyEarningsContext OpenQuarterlyEarnings()
    {
        Log.Info("Navigate to Quarterly Earnings from Home.");
        page.OpenQuarterlyEarnings();
        return new QuarterlyEarningsContext();
    }

    public ServicesContext OpenServiceCategory(string categoryName)
    {
        Log.Info($"Navigate to Services category '{categoryName}' from Home.");
        page.OpenServiceCategory(categoryName);
        return new ServicesContext();
    }
    public HomeContext Search(string searchTerm)
    {
        Log.Info($"Global search: '{searchTerm}'.");
        page.Search(searchTerm);
        return this;
    }
    
    public HomeContext ValidateSearchResultsContain(string searchTerm)
    {
        Log.Info($"Validate search results contain: '{searchTerm}'.");
        page.ValidateSearchResultsContain(searchTerm);
        return this;
    }

    public bool AreSearchResultsContain(string searchTerm)
    {
        Log.Info($"Check search results contain: '{searchTerm}'.");
        return page.AreSearchResultsContain(searchTerm);
    }
    
    public bool IsOpened()
    {
        return page.IsOpened();
    }

    public void ValidateOpened()
    {
        Log.Info("Validate Home page opened.");
        ValidationRules.ValidatePageState(IsOpened(), "Home page was not opened.");
    }
}

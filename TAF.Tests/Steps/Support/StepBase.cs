using Reqnroll;
using TAF.Business.Business;

namespace TAF.Tests.Steps.Support;

public abstract class StepBase
{
    protected readonly ScenarioContext ScenarioContext;

    protected StepBase(ScenarioContext scenarioContext)
    {
        ScenarioContext = scenarioContext;
    }

    protected HomeContext GetHome()
    {
        if (ScenarioContext.TryGetValue(ScenarioKeys.HomeContext, out HomeContext? home) && home != null)
        {
            return home;
        }

        home = new HomeContext();
        ScenarioContext[ScenarioKeys.HomeContext] = home;
        return home;
    }

    protected CareersContext GetCareers()
    {
        if (ScenarioContext.TryGetValue(ScenarioKeys.CareersContext, out CareersContext? careers) && careers != null)
        {
            return careers;
        }

        careers = new CareersContext();
        ScenarioContext[ScenarioKeys.CareersContext] = careers;
        return careers;
    }

    protected InsightsContext GetInsights()
    {
        if (ScenarioContext.TryGetValue(ScenarioKeys.InsightsContext, out InsightsContext? insights) && insights != null)
        {
            return insights;
        }

        insights = new InsightsContext();
        ScenarioContext[ScenarioKeys.InsightsContext] = insights;
        return insights;
    }

    protected QuarterlyEarningsContext GetQuarterlyEarnings()
    {
        if (ScenarioContext.TryGetValue(ScenarioKeys.QuarterlyEarningsContext, out QuarterlyEarningsContext? quarterly) && quarterly != null)
        {
            return quarterly;
        }

        quarterly = new QuarterlyEarningsContext();
        ScenarioContext[ScenarioKeys.QuarterlyEarningsContext] = quarterly;
        return quarterly;
    }
}

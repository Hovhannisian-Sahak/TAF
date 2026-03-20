using Reqnroll;
using TAF.Core.Api;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Tests.Steps.Support;

namespace TAF.Tests.Hooks;

[Binding]
public sealed class ApiHooks
{
    private static readonly log4net.ILog Log = AppLogger.For<ApiHooks>();
    private readonly ScenarioContext scenarioContext;

    public ApiHooks(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [BeforeScenario("api", Order = 0)]
    public void BeforeScenario()
    {
        _ = Configuration.Api; // force configuration + logging setup
        EnsureLogDirectory();
        Log.Info($"Starting API scenario: {scenarioContext.ScenarioInfo.Title}");

        var client = new ApiClient(Configuration.Api.BaseUrl);
        scenarioContext[ScenarioKeys.ApiClient] = client;
    }

    [AfterScenario("api")]
    public void AfterScenario()
    {
        Log.Info($"Finished API scenario: {scenarioContext.ScenarioInfo.Title} - {scenarioContext.ScenarioExecutionStatus}");
    }

    private static void EnsureLogDirectory()
    {
        var logPath = Configuration.Logging.FilePath;
        var directory = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}

using NUnit.Framework;
using TAF.Business.Business;
using TAF.Core.Logging;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

public class GlobalSearchTests: UiTestBase
{
    private static readonly log4net.ILog Log = AppLogger.For<GlobalSearchTests>();

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    public void User_Can_Use_Global_Search(string searchTerm)
    {
        Log.Info($"Test start: global search '{searchTerm}'.");
        new HomeContext()
            .OpenHome()
            .Search(searchTerm)
            .ValidateSearchResultsContain(searchTerm);
    }
}

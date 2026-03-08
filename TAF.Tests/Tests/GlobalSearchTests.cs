using NUnit.Framework;
using TAF.Business.Business;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

public class GlobalSearchTests: UiTestBase
{
    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    public void User_Can_Use_Global_Search(string searchTerm)
    {
        new HomeContext()
            .OpenHome()
            .Search(searchTerm)
            .ValidateSearchResultsContain(searchTerm);
    }
}

using NUnit.Framework;
using TAF.Business.Business;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

[TestFixture]
public class CareersSearchTests : UiTestBase
{
    [TestCase("Java", "All Locations")]
    [TestCase("C#", "All Locations")]
    public void User_Can_Search_Position_And_Open_Latest_Result(string programmingLanguage, string location)
    {
        new HomeContext()
            .OpenHome()
            .OpenCareers()
            .SearchByCriteria(programmingLanguage, location, remoteOnly: true)
            .OpenLatestResultAndViewApply()
            .ValidateKeywordPresentOnDetails(programmingLanguage);
    }
}

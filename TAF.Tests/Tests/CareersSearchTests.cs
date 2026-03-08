using NUnit.Framework;
using TAF.Business.Business;
using TAF.Core.Logging;
using TAF.Tests.TestBase;

namespace TAF.Tests.Tests;

[TestFixture]
public class CareersSearchTests : UiTestBase
{
    private static readonly log4net.ILog Log = AppLogger.For<CareersSearchTests>();

    [TestCase("Java", "Belarus")]
    [TestCase("C#", "Armenia")]
    public void User_Can_Search_Position_And_Open_Latest_Result(string programmingLanguage, string location)
    {
        Log.Info($"Test start: search '{programmingLanguage}' in '{location}'.");
        new HomeContext()
            .OpenHome()
            .OpenCareers()
            .SearchByCriteria(programmingLanguage, location)
            .OpenLatestResultAndViewApply()
            .ValidateKeywordPresentOnDetails(programmingLanguage)
            .ValidateCountryNamePresentOnDetails(location);
    }
}

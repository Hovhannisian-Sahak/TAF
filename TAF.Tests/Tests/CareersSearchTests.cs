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
        var careers = new HomeContext()
            .OpenHome()
            .OpenCareers()
            .SearchByCriteria(programmingLanguage, location)
            .OpenLatestResultAndViewApply();

        Assert.Multiple(() =>
        {
            Assert.That(careers.IsKeywordPresentOnDetails(programmingLanguage), Is.True,
                $"Expected keyword '{programmingLanguage}' to be present on vacancy details page.");
            Assert.That(careers.IsCountryNamePresentOnDetails(location), Is.True,
                $"Expected location '{location}' to be present on vacancy details page.");
        });
    }
}

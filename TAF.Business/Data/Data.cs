using OpenQA.Selenium;

namespace TAF.Business.Data;

public static class Data
{
    public const string HomeRelativeUrl = "/";
    public const string CareersRelativeUrl = "/careers";
    public const string InsightsRelativeUrl = "/insights";

    public const string CareersTitleKeyword = "career";
    public const string InsightsTitleKeyword = "insight";

    public static readonly By HomeRoot = By.TagName("body");
    public static readonly By CareersNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'careers') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'career')]");
    public static readonly By InsightsNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insights') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insight')]");

    public static readonly By CareersHeader = By.XPath("//h1[contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'career')]");
    public static readonly By InsightsHeader = By.XPath("//h1[contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'insight')]");
}

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

    public static readonly By CookieAcceptAllButton = By.XPath(
        "//button[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'accept all')] | " +
        "//a[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'accept all')]");

    public static readonly By CareersNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'careers') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'career') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'carrier')]");

    public static readonly By InsightsNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insights') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insight')]");

    public static readonly By CareersHeader = By.XPath("//h1[contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'career')]");
    public static readonly By InsightsHeader = By.XPath("//h1[contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'insight')]");
    public static readonly By StartSearchHereButton = By.XPath(
        "//a[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'start your search here')] | " +
        "//button[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'start your search here')]");

    public static readonly By KeywordInput = By.XPath(
        "//input[contains(translate(@placeholder,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'keyword') or contains(translate(@aria-label,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'keyword') or contains(translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'keyword')]");

    public static readonly By LocationInput = By.XPath(
        "//input[contains(translate(@placeholder,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'location') or contains(translate(@aria-label,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'location') or contains(translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'location')]");

    public static readonly By RemoteOption = By.XPath(
        "//*[self::label or self::span or self::div][contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'remote')]");

    public static readonly By FindButton = By.XPath(
        "//button[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'find')]");

    public static readonly By SearchResultsCards = By.XPath(
        "//*[contains(@class,'search-result') or contains(@class,'vacancy') or contains(@class,'job')]");

    public static readonly By ViewAndApplyButtons = By.XPath(
        "//a[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'view and apply')] | " +
        "//button[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'view and apply')]");

    public static readonly By ResultTitleLinks = By.XPath(
        "//a[contains(@href,'job') and string-length(normalize-space(.)) > 0]");
}

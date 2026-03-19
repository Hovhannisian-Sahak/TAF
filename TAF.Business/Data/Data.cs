using OpenQA.Selenium;

namespace TAF.Business.Data;

public static class Data
{
    public const string HomeRelativeUrl = "/";
    public const string CareersRelativeUrl = "/careers";
    public const string InsightsRelativeUrl = "/insights";
    public const string AboutRelativeUrl = "/about";
    public const string QuarterlyEarningsRelativeUrl = "/quarterly-earnings";
    
    public const string CareersTitleKeyword = "career";
    public const string InsightsTitleKeyword = "insight";
    public const string QuarterlyEarningsTitleKeyword = "quarterly earnings";
    public const string QuarterlyEarningsDownloadUrlContains = "436759741/files/doc_financials/2025/";
    public static readonly By HomeRoot = By.TagName("body");

    public static readonly By CookieAcceptAllButton = By.Id("onetrust-accept-btn-handler");

    public static readonly By CareersNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'careers') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'career')]");

    public static readonly By InsightsNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insights') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'insight')]");
    
    public static readonly By AboutUsNavigationLink = By.XPath(
        "//header//a[normalize-space()='About']");
    
    public static readonly By QuarterlyEarningsNavigationLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'quarterly-earnings') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'quarterly-earnings')]");
    
    public static readonly By QuarterlyEarningsDownloadLink = By.XPath(
        "//a[contains(translate(@href,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'436759741/files/doc_financials/2025') or contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'436759741/files/doc_financials/2025')]");
    
    public static readonly By CareersHeader = By.XPath("//*[@id='wrapper']//a[contains(text(),'Careers')]");
    public static readonly By InsightsHeader = By.XPath("//*[@id='wrapper']//a[contains(text(),'Insights')]");
    public static readonly By QuarterlyEarningsHeader = By.XPath("//div[contains(@class,'module-breadcrumb')]//*[normalize-space()='Quarterly Earnings']");
    public static readonly By StartSearchButton = By.XPath("//*[@data-gtm-category='job_search_redirect']");

    public static readonly By InsightsReadMoreLink = By.XPath("//div[contains(@class,'slider-ui-23') and @data-configuration='single-full-width']//div[contains(@class,'owl-item') and contains(@class,'active') and @aria-hidden='false']//a[contains(@class,'slider-cta-link')]");
    public static readonly By InsightsCarouselNextButton = By.ClassName("slider__right-arrow");
    public static readonly By ArticleHeading = By.XPath("//h1[normalize-space()]");

    public static readonly By KeywordInput = By.CssSelector("#anchor-list-wrapper .SearchBox_input__sJnt2");
    
    public static readonly By LocationInput = By.CssSelector("input[aria-label='Choose your country']");

    public static readonly By RemoteOption = By.CssSelector("input[type='checkbox'][name='vacancy_type-Remote']");

    public static readonly By FindButton = By.CssSelector(".SearchBox_button__3ImF7");

    public static readonly By SearchResultsCards = By.ClassName("List_list___59gh");
    
    public static readonly By CardExpandButton = By.CssSelector("span[data-testid='accordion-section-header-icon-container']");

    public static readonly By ApplyButton = By.Id("cta_job_apply_unauthorized");
    
    public static readonly By CloseModalButton = By.ClassName("ApplyWizardModal_closeButton__DDgiS");
    
    public static readonly By GlobalSearchIcon = By.ClassName("search-icon");
    
    public  static readonly By GlobalSearchInput = By.Id("new_form_search");
    
    public static readonly By GlobalSearchButton = By.ClassName("custom-search-button");
    
    public static readonly By SearchResultTexts = By.XPath("//*[contains(@class,'search-results__items')]//p[normalize-space()]");
    
    public static readonly By CarouselSlideArticelTitle = By.XPath("//div[contains(@class,'slider-ui-23') and @data-configuration='single-full-width']//div[contains(@class,'owl-item') and contains(@class,'active') and @aria-hidden='false']//div[contains(@class,'single-slide-ui')]//*[contains(@class,'scaling-of-text-wrapper')]");

    public static readonly By CarouselSlideArticleHeader = By.CssSelector("h1.scaling-of-text-wrapper");

    public static readonly By PropertyButton = By.Id("ink");

    public static readonly By PropertyModal = By.Id("dialog");
    public static readonly By PropertiesLink = By.Id("properties-button");

    public static readonly By PropertiesPopupHeader = By.XPath("//div[@slot='title']");
    
    public static readonly By QuarterlyEarningsFrames = By.Name("820B7564B344812621A9FDBB3EB9C705");
    // public static readonly By DownloadEarningsLink = By.XPath("(//div[contains(@class,'annual-link')])[2]//a");
}

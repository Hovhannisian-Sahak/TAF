using System;
using System.Linq;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TAF.Core.WebDriver;


namespace TAF.Core.WebElement
{
    public abstract class BaseElement
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseElement));
        protected IWebDriver Driver => WebDriverWrapper.Driver;

        private DefaultWait<IWebDriver> CreateWait(int? timeoutSeconds = null)
        {
            var wait = new DefaultWait<IWebDriver>(Driver)
            {
                Timeout = TimeSpan.FromSeconds(timeoutSeconds ?? Configuration.Configuration.Timeouts.Default),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));

            return wait;
        }

        // =========================
        // CORE ELEMENT ACCESS
        // =========================

        protected IWebElement Element(By locator, int? timeoutSeconds = null)
        {
            Log.Debug($"Find element: {locator}");
            return CreateWait(timeoutSeconds)
                .Until(driver => driver.FindElement(locator));
        }
        
        protected IReadOnlyList<IWebElement> Elements(By locator, int? timeoutSeconds = null)
        {
            Log.Debug($"Find elements: {locator}");
            return CreateWait(timeoutSeconds)
                .Until(driver =>
                {
                    var elements = driver.FindElements(locator);
                    return elements.Count > 0 ? elements : null;
                })!;
        }
        
        protected void ScrollIntoView(By locator, int? timeoutSeconds = null)
        {
            var element = Element(locator, timeoutSeconds);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
        }

        protected void ScrollIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
        }

        protected void Hover(By locator, int? timeoutSeconds = null)
        {
            Log.Debug($"Hover element: {locator}");
            var element = Element(locator, timeoutSeconds);
            new Actions(Driver).MoveToElement(element).Perform();
        }

        protected IWebElement WaitUntilClickable(By locator, int? timeoutSeconds = null)
        {
            Log.Debug($"Wait until clickable: {locator}");

            var element = CreateWait(timeoutSeconds)
                .Until(driver => driver.FindElement(locator)); // find in DOM

            // Scroll into viewport
            ScrollIntoView(element);

            // Now wait until actually visible and enabled
            return CreateWait(timeoutSeconds)
                .Until(driver => element.Displayed && element.Enabled ? element : null);
        }

        protected IWebElement VisibleElement(By locator, int? timeoutSeconds = null)
        {
            Log.Debug($"Wait until visible element: {locator}");
            return CreateWait(timeoutSeconds)
                .Until(driver =>
                {
                    var elements = driver.FindElements(locator);
                    return elements.FirstOrDefault(e => e.Displayed);
                })!;
        }

        // =========================
        // CONDITION-BASED WAITS
        // =========================

        protected void WaitUntilTextContains(
            By locator,
            string text,
            int? timeoutSeconds = null)
        {
            Log.Debug($"Wait until element text contains '{text}': {locator}");
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.FindElement(locator).Text.Contains(text));
        }

        protected void WaitUntilAttributeContains(
            By locator,
            string attribute,
            string value,
            int? timeoutSeconds = null)
        {
            Log.Debug($"Wait until attribute '{attribute}' contains '{value}': {locator}");
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.FindElement(locator)
                          .GetAttribute(attribute)?
                          .Contains(value) == true);
        }

        protected void WaitUntilUrlContains(
            string partialUrl,
            int? timeoutSeconds = null)
        {
            Log.Debug($"Wait until URL contains '{partialUrl}'");
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.Url.Contains(partialUrl));
        }

        // =========================
        // RETRY LOGIC (Improved)
        // =========================

        protected void Retry(Action action, int? retries = null)
        {
            var maxRetries = retries ?? Configuration.Configuration.Timeouts.Retry;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception) when (i < maxRetries - 1)
                {
                    Log.Warn($"Action failed on retry {i + 1} of {maxRetries}. Retrying.");
                }
            }
        }
    }
}

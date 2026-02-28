using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TAF.Core.WebDriver;


namespace TAF.Core.WebElement
{
    public abstract class BaseElement
    {
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
            return CreateWait(timeoutSeconds)
                .Until(driver => driver.FindElement(locator));
        }

        protected void WaitUntilClickable(By locator, int? timeoutSeconds = null)
        {
            CreateWait(timeoutSeconds)
                .Until(driver =>
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed && element.Enabled ? element : null;
                });
        }

        // =========================
        // CONDITION-BASED WAITS
        // =========================

        protected void WaitUntilTextContains(
            By locator,
            string text,
            int? timeoutSeconds = null)
        {
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
                    //swallow exception and retry
                }
            }
        }
    }
}

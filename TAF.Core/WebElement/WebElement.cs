using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TAF.Core.WebDriver;

namespace TAF.Core.WebElement
{
    public abstract class BaseElement
    {
        protected IWebDriver Driver => WebDriverWrapper.Driver;

        private DefaultWait<IWebDriver> CreateWait(int timeoutSeconds = Timeouts.Default)
        {
            var wait = new DefaultWait<IWebDriver>(Driver)
            {
                Timeout = TimeSpan.FromSeconds(timeoutSeconds),
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

        protected IWebElement Element(By locator, int timeoutSeconds = Timeouts.Default)
        {
            return CreateWait(timeoutSeconds)
                .Until(driver => driver.FindElement(locator));
        }

        protected void WaitUntilClickable(By locator, int timeoutSeconds = Timeouts.Default)
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
            int timeoutSeconds = Timeouts.Default)
        {
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.FindElement(locator).Text.Contains(text));
        }

        protected void WaitUntilAttributeContains(
            By locator,
            string attribute,
            string value,
            int timeoutSeconds = Timeouts.Default)
        {
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.FindElement(locator)
                          .GetAttribute(attribute)?
                          .Contains(value) == true);
        }

        protected void WaitUntilUrlContains(
            string partialUrl,
            int timeoutSeconds = Timeouts.Default)
        {
            CreateWait(timeoutSeconds)
                .Until(driver =>
                    driver.Url.Contains(partialUrl));
        }

        // =========================
        // RETRY LOGIC (Improved)
        // =========================

        protected void Retry(Action action, int retries = Timeouts.Retry)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception) when (i < retries - 1)
                {
                    //swallow exception and retry
                }
            }
        }
    }
}
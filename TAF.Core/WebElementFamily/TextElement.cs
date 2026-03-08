using System;
using OpenQA.Selenium;

namespace TAF.Core.WebElementFamily
{
    public class TextElement : WebElement.BaseElement
    {
        private readonly By _locator;

        public TextElement(By locator)
        {
            _locator = locator;
        }

        public string Text => Element(_locator).Text;
    }
}

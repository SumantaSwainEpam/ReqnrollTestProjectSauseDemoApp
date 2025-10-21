using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ReqnrollTestProjectSauseDemoApp.Helper
{
    public class SeleniumHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SeleniumHelper(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        // ---------------- Element Finders ----------------
        public IWebElement Find(By locator, bool requireDisplayed = true)
        {
            return _wait.Until(driver =>
            {
                var el = driver.FindElement(locator);
                return requireDisplayed ? (el.Displayed ? el : null) : el;
            });
        }

        public IWebElement WaitUntilClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        // ---------------- Actions ----------------
        public void Click(By locator)
        {
            try
            {
                Find(locator).Click();
            }
            catch (StaleElementReferenceException)
            {
                Find(locator).Click();
            }
        }

        public void ClickElement(IWebElement element)
        {
            _wait.Until(driver => element.Displayed && element.Enabled);
            element.Click();
        }

        public void EnterText(By locator, string text)
        {
            var el = Find(locator);
            el.Clear();
            el.SendKeys(text);
        }

        public void EnterTextElement(IWebElement element, string text)
        {
            _wait.Until(driver => element.Displayed && element.Enabled);
            element.Clear();
            element.SendKeys(text);
        }

        public void PressEnter(By locator)
        {
            Find(locator).SendKeys(Keys.Enter);
        }

        public string GetText(By locator)
        {
            return Find(locator).Text;
        }

        public bool IsDisplayed(By locator)
        {
            try
            {
                return Find(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        // ---------------- Advanced Utilities ----------------
        public void Hover(By locator)
        {
            var element = Find(locator);
            var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
            actions.MoveToElement(element).Perform();
        }

        public void ScrollToElement(By locator)
        {
            var element = Find(locator);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void SelectDropdownByText(By locator, string text)
        {
            var element = Find(locator);
            var select = new SelectElement(element);
            select.SelectByText(text);
        }

        public void SelectDropdownByValue(By locator, string value)
        {
            var element = Find(locator);
            var select = new SelectElement(element);
            select.SelectByValue(value);
        }

        public bool WaitForElementInvisible(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public void TakeScreenshot(string filePath)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ReqnrollTestProjectSauseDemoApp.Drivers;
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

        public IReadOnlyCollection<IWebElement> FindAll(By locator)
        {
            return _driver.FindElements(locator);
        }

        public IWebElement WaitUntilVisible(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitUntilInvisible(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return !element.Displayed ? element : null;
            });
        }

        public IWebElement IsDisplayed(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Displayed ? element : null;
            });
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

        public IWebElement WaitUntilClickable(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
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

        // ---------------- Advanced Utilities ----------------
        public void Hover(By locator)
        {
            var element = Find(locator);
            var actions = new Actions(_driver);
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

        public string CaptureScreenShot()
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)WebDriverFactory.GetWebDriver();
            var image = screenshotDriver.GetScreenshot();
            return image.AsBase64EncodedString;
        }

        // ---------------- Browser Utilities ----------------
        /// <summary>
        /// Returns the current URL of the active browser window.
        /// </summary>
        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        /// <summary>
        /// Returns the title of the current page.
        /// </summary>
        public string GetPageTitle()
        {
            return _driver.Title;
        }
    }
}

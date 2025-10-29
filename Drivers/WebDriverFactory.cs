using OpenQA.Selenium;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public static class WebDriverFactory
    {
        private static readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();
        public static IWebDriver CreateDriver(string browserType)
        {
            if (_driver.Value == null)
            {
                IDriverFactory factory = browserType.ToLower() switch
                {
                    "chrome" => new ChromeDriverFactory(),
                    "edge" => new EdgeDriverFactory(),
                    "firefox" => new FirefoxDriverFactory(),
                    _ => throw new ArgumentException($"Unsupported browser type:{browserType}")
                };

                _driver.Value = factory.CreateWebDriver();
            }
            return _driver.Value;
        }

        public static IWebDriver GetWebDriver()
        {
            return _driver.Value ?? throw new InvalidOperationException("WebDriver has not been created. Call CreateDriver first.");
        }


        public static void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value?.Quit();
                _driver.Value?.Dispose();
                _driver.Value = null!;
            }
        }

    }
}

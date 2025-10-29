using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public class FirefoxDriverFactory : IDriverFactory
    {
        public IWebDriver CreateWebDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-popup-blocking");
            var driver = new FirefoxDriver(options);
            driver.Manage().Window.Maximize();
            return driver;

        }
    }

}

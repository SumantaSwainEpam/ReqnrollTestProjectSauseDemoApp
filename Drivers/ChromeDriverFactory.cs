using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public class ChromeDriverFactory : IDriverFactory
    {

        public IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--guest");
            options.AddArgument("--disable-popup-blocking");
            var driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;

        }
    }
}

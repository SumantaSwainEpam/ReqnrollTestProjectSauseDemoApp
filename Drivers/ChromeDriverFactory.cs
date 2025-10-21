using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public class ChromeDriverFactory: IDriverFactory
    {

        public IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            options.AddArgument("--guest");
            options.AddArgument("--disable-popup-blocking");
            var driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;

        }
    }
}

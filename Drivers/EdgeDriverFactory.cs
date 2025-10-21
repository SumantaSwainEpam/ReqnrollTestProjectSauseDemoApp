using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public class EdgeDriverFactory: IDriverFactory
    {
        public IWebDriver CreateWebDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-popup-blocking");
            var driver = new EdgeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}

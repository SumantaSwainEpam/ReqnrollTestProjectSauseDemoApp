using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public interface IDriverFactory
    {
        IWebDriver CreateWebDriver();
    }
}

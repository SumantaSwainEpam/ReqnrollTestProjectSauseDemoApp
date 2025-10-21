using OpenQA.Selenium;
using Reqnroll.BoDi;
using ReqnrollTestProjectSauseDemoApp.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProjectSauseDemoApp.Hooks
{

    [Binding]
    public sealed class Hook
    {
        private readonly IObjectContainer _objectConatiner;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        public Hook(IObjectContainer objectContainer,ScenarioContext scenarioContext)
        {
            _objectConatiner = objectContainer;
            _scenarioContext = scenarioContext;

            
        }

        [BeforeScenario(Order = 0)]
        public void FirstBeforeScenario()
        {
            string browserType = "chrome";
            _driver=WebDriverFactory.CreateDriver(browserType);
            _objectConatiner.RegisterInstanceAs<IWebDriver>(_driver);




        }

        public IWebDriver GetDriver() => _driver;


        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            WebDriverFactory.QuitDriver();
        }   

    }
}

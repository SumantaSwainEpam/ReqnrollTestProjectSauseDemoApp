using OpenQA.Selenium;
using Reqnroll.BoDi;
using ReqnrollTestProjectSauseDemoApp.Credentials;
using ReqnrollTestProjectSauseDemoApp.Drivers;
using ReqnrollTestProjectSauseDemoApp.Pages;
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

        public void LoginToApplication(string username, string password)
        {
            WebDriverFactory.GetWebDriver().Navigate().GoToUrl(CredentialsManager.GetBaseUrl());
            var loginPage = new LoginPage();
            var finalUsername = string.IsNullOrEmpty(username)
                ? CredentialsManager.GetDefaultUsername()
                : username;

            var finalPassword = string.IsNullOrEmpty(password)
                ? CredentialsManager.GetDefaultPassword()
                : password;

            loginPage.EnterUsername(finalUsername);
            loginPage.EnterPassword(finalPassword);
            loginPage.ClickLoginButton();
            loginPage.IsAppLogoDisplayed();

        }

        [BeforeScenario(Order = 0)]
        public void FirstBeforeScenario()
        {
            //string browserType = CredentialsManager.GetDefaultBrowser();
            string browserType = CredentialsManager.GetBrowserByIndex(0);
            _driver =WebDriverFactory.CreateDriver(browserType);
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

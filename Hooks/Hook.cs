using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll.BoDi;
using ReqnrollTestProjectSauseDemoApp.Credentials;
using ReqnrollTestProjectSauseDemoApp.Drivers;
using ReqnrollTestProjectSauseDemoApp.Helper;
using ReqnrollTestProjectSauseDemoApp.Pages;
using ReqnrollTestProjectSauseDemoApp.Reports;

namespace ReqnrollTestProjectSauseDemoApp.Hooks
{

    [Binding]
    public sealed class Hook
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private ExtentReports _extentReports;
        private ExtentTest _stepTest;
        private ExtentTest _extentTest;
        private IWebDriver _driver;
        public Hook(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
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
            _driver = WebDriverFactory.CreateDriver(browserType);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);


            string scenarioTitle = _scenarioContext.ScenarioInfo.Title;
            Logs.LogManager.InitializeLogging(scenarioTitle);
            Logs.LogManager.GetLogger().Info($"Starting scenario: {scenarioTitle} On Browser: {browserType}");


            _extentReports = ReportManager.GetReports(scenarioTitle);
            _extentTest = _extentReports.CreateTest(scenarioTitle);



            if (!_objectContainer.IsRegistered<Hook>())
            {
                _objectContainer.RegisterInstanceAs(this);
            }

        }

        public IWebDriver GetDriver() => _driver;


        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {

            var logger = Logs.LogManager.GetLogger();

            try
            {

                var driver = _objectContainer.Resolve<IWebDriver>();

                if (_scenarioContext.TestError != null)
                    logger.Error($"❌ Test Failed: {_scenarioContext.ScenarioInfo.Title}");
                else
                    logger.Info($"✅ Test Passed: {_scenarioContext.ScenarioInfo.Title}");


                WebDriverFactory.QuitDriver();
            }
            catch (Exception ex)
            {
                logger.Error($"⚠️ Error while cleaning up driver: {ex.Message}");
            }


        }

        [AfterStep]
        public void AfetrStep()
        {
            var stepInfo = _scenarioContext.StepContext.StepInfo.Text;
            var stepStatus = _scenarioContext.TestError == null ? Status.Pass : Status.Fail;
            _stepTest = _extentTest.CreateNode(stepInfo);
            _stepTest.Log(stepStatus, stepInfo);

            if (_scenarioContext.TestError != null)
            {
                _stepTest.Fail($"Error: {_scenarioContext.TestError.Message}");
                _stepTest.AddScreenCaptureFromBase64String(new SeleniumHelper(_driver).CaptureScreenShot());
            }

        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportManager.FlushReports();
            Logs.LogManager.GetLogger().Info($"Extent Report has been flushed.");
        }



    }
}

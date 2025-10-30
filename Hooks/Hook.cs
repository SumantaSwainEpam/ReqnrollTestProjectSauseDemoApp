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
        private IWebDriver _driver;
        private ExtentTest _scenarioTest;

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


            // Get shared report instance and create a new test node
            var extentReports = ReportManager.GetReports();
            _scenarioTest = extentReports.CreateTest(scenarioTitle);

            // Register test node in ScenarioContext (for later step logging)
            _scenarioContext["ExtentTest"] = _scenarioTest;




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
        public void AfterStep()
        {
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            var stepStatus = _scenarioContext.TestError == null ? Status.Pass : Status.Fail;

            if (_scenarioContext.TryGetValue("ExtentTest", out ExtentTest extentTest))
            {
                var stepNode = extentTest.CreateNode(stepText);
                stepNode.Log(stepStatus, stepText);

                if (_scenarioContext.TestError != null)
                {
                    stepNode.Fail($"❌ Error: {_scenarioContext.TestError.Message}");
                    stepNode.AddScreenCaptureFromBase64String(
                        new SeleniumHelper(_driver).CaptureScreenShot(),
                        "Screenshot on Failure"
                    );
                }
            }
        }




        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportManager.FlushReports();
            Logs.LogManager.GetLogger().Info($"📘 Extent Report has been flushed to: {ReportManager.GetReportPath()}");

        }



    }
}

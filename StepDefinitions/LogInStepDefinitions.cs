using FluentAssertions;
using ReqnrollTestProjectSauseDemoApp.Credentials;
using ReqnrollTestProjectSauseDemoApp.Drivers;
using ReqnrollTestProjectSauseDemoApp.Hooks;
using ReqnrollTestProjectSauseDemoApp.Pages;

namespace ReqnrollTestProjectSauseDemoApp.StepDefinitions
{
    [Binding]
    public class LogInStepDefinitions
    {
        private LoginPage _loginPage;

        public LogInStepDefinitions(Hook hook)
        {

            _loginPage = new LoginPage();

        }

        [Given("I am on the Sauce Demo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            WebDriverFactory.GetWebDriver().Navigate().GoToUrl(CredentialsManager.GetBaseUrl());
        }

        [When("I enter username {string}")]
        public void WhenIEnterUsername(string p0)
        {
            var finalUsername = string.IsNullOrEmpty(p0)
                ? CredentialsManager.GetDefaultUsername()
                : p0;

            _loginPage.EnterUsername(finalUsername);

        }

        [When("I enter password {string}")]
        public void WhenIEnterPassword(string p0)
        {
            var finalPassword = string.IsNullOrEmpty(p0)
                 ? CredentialsManager.GetDefaultPassword()
                 : p0;
            _loginPage.EnterPassword(finalPassword);
        }

        [When("I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            _loginPage.ClickLoginButton();

        }

        [Then("I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            _loginPage.IsAppLogoDisplayed().Should().BeTrue();
        }

        [Then("I should see the products list")]
        public void ThenIShouldSeeTheProductsList()
        {
            _loginPage.GetAppLogoText().Should().Be(CredentialsManager.GetAppTitle());

        }
    }
}

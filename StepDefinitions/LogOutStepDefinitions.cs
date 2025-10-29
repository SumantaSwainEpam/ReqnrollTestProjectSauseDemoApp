using System;
using Reqnroll;
using ReqnrollTestProjectSauseDemoApp.Hooks;
using ReqnrollTestProjectSauseDemoApp.Pages;

namespace ReqnrollTestProjectSauseDemoApp.StepDefinitions
{
    [Binding]
    public class LogOutStepDefinitions
    {   
        private readonly LogoutPage _logoutPage;
        private readonly Hook _hook;
        public LogOutStepDefinitions(Hook hook)
        {
            _logoutPage = new LogoutPage();
            _hook = hook;

        }
        [Given("I am logged in as {string} with {string}")]
        public void GivenIAmLoggedInAsWith(string p0, string p1)
        {
            _hook.LoginToApplication(p0, p1);

        }

        [When("I click on the menu button")]
        public void WhenIClickOnTheMenuButton()
        {
           _logoutPage.ClickMenuButton();
        }

        [When("I select the logout option")]
        public void WhenISelectTheLogoutOption()
        {
            _logoutPage.ClickLogoutLink();
        }

        [Then("I should be redirected to the login page")]
        public void ThenIShouldBeRedirectedToTheLoginPage()
        {
           _logoutPage.VerifyUserIsLoggedOut();
        }

        [Then("I should see the login form")]
        public void ThenIShouldSeeTheLoginForm()
        {    
            _logoutPage.VerifyUserAbleToSeeLoginForm();
            _logoutPage.VerifySwagLabTextDisplayed();
        }
    }
}

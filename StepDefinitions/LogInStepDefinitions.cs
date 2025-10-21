using System;
using Reqnroll;

namespace ReqnrollTestProjectSauseDemoApp.StepDefinitions
{
    [Binding]
    public class LogInStepDefinitions
    {
        [Given("I am on the Sauce Demo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            throw new PendingStepException();
        }

        [When("I enter username {string}")]
        public void WhenIEnterUsername(string p0)
        {
            throw new PendingStepException();
        }

        [When("I enter password {string}")]
        public void WhenIEnterPassword(string p0)
        {
            throw new PendingStepException();
        }

        [When("I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            throw new PendingStepException();
        }

        [Then("I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            throw new PendingStepException();
        }

        [Then("I should see the products list")]
        public void ThenIShouldSeeTheProductsList()
        {
            throw new PendingStepException();
        }
    }
}

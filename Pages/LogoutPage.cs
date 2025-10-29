using FluentAssertions;
using OpenQA.Selenium;
using ReqnrollTestProjectSauseDemoApp.Drivers;
using ReqnrollTestProjectSauseDemoApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProjectSauseDemoApp.Pages
{
    public class LogoutPage
    {
        private readonly SeleniumHelper _helper;

        public LogoutPage()
        {
            _helper=new SeleniumHelper(WebDriverFactory.GetWebDriver());
        }

        #region Locators
        private By MenuButton => By.Id("react-burger-menu-btn");
        private By LogoutLink => By.Id("logout_sidebar_link");
        private By SwagLabText => By.XPath("//div[text()='Swag Labs']");
        private By LoginButton => By.Id("login-button");
        private By LoginForm => By.Id("login_button_container");



        #endregion

        /// <summary>
        /// Click the menu button to open the sidebar menu
        /// </summary>
        public void ClickMenuButton()
        {
            _helper.IsDisplayed(MenuButton).Should().BeTrue("Menu button should be visible before clicking.");
            _helper.Click(MenuButton);
        }

        /// <summary>
        /// Click the logout link to log out of the application
        /// </summary>
        public void ClickLogoutLink()
        {
            _helper.IsDisplayed(LogoutLink).Should().BeTrue("Logout link should be visible before clicking.");
            _helper.Click(LogoutLink);
        }

        /// <summary>
        /// Verifies the user is redirected to login page after logout
        /// </summary>
        public void VerifyUserIsLoggedOut()
        {
            // Verify the "Login" button is visible
            _helper.IsDisplayed(LoginButton)
                .Should().BeTrue("Login button should be visible after logout.");

            // Verify URL (if you want to ensure redirection)
            string currentUrl = _helper.GetCurrentUrl();
            currentUrl.Should().Contain("saucedemo.com", "User should be redirected to the login page after logout.");

        }

        /// <summary>
        /// Verify that "Swag Labs" header is displayed (pre-login page confirmation)
        /// </summary>
        public void VerifySwagLabTextDisplayed()
        {
            _helper.IsDisplayed(SwagLabText)
                .Should().BeTrue("'Swag Labs' text should be visible on the login page after logout.");
        }
        /// <summary>
        /// Verify User is able to see Log in form of Swag Labs App
        /// </summary>
        public void VerifyUserAbleToSeeLoginForm()
        {
             LoginForm.Should().NotBeNull();
            _helper.IsDisplayed(LoginForm).Should().BeTrue("");

        }


    }
}

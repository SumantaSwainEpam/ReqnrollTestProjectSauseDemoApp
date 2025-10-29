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
    public class LoginPage
    {
        private readonly SeleniumHelper _helper;
        public LoginPage()
        {
            _helper = new SeleniumHelper(WebDriverFactory.GetWebDriver());
        }

        #region Locators
        private By _usernameInput =>By.Id("user-name");
        private By _passwordInput => By.Id("password");
        //private By _loginButton => By.Id("login-button");
        private IWebElement _loginButton => _helper.Find(By.Id("login-button"));
        private By AppLogo => By.CssSelector(".app_logo");
        #endregion

        /// <summary>
        ///  Enter username into the username input field
        /// </summary>
        /// <param name="username"></param>
        public void EnterUsername(string username)
        {
            _helper.EnterText(_usernameInput, username);
        }
        
        /// <summary>
        ///  Enter password into the password input field
        /// </summary>
        /// <param name="password"></param>
        public void EnterPassword(string password)
        {
            _helper.EnterText(_passwordInput, password);
        }

        /// <summary>
        /// Click the login button
        /// </summary>
        public void ClickLoginButton()
        {
            _helper.ClickElement(_loginButton);
           // _helper.Click(_loginButton);
        }

        /// <summary>
        /// Determines whether the application logo is currently displayed in the user interface.
        /// </summary>
        /// <returns>true if the application logo is visible; otherwise, false.</returns>
        public bool IsAppLogoDisplayed()
        {
            return _helper.IsDisplayed(AppLogo);
        }

        /// <summary>
        /// Retrieves the display text associated with the application's logo.
        /// </summary>
        /// <returns>A string containing the text for the application logo. Returns an empty string if no logo text is available.</returns>
        public string GetAppLogoText()
        {
            return _helper.GetText(AppLogo);
        }

    }
}

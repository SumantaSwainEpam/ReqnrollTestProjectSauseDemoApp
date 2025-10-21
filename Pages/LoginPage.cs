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


    }
}

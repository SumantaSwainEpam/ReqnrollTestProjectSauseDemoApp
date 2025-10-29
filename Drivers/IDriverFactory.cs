using OpenQA.Selenium;

namespace ReqnrollTestProjectSauseDemoApp.Drivers
{
    public interface IDriverFactory
    {
        IWebDriver CreateWebDriver();
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Login
{
    [TestClass]
    public class LogoutTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestSuccessfullLogout()
        {
            using var webDriver = Browser.GetChrome();

            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());

            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            webApp.Logout();

            bool loginbuttonExists = webDriver.CheckIfExists(XPath.Attribute("href", "/login"));
            Assert.IsTrue(loginbuttonExists);
        }
    }
}

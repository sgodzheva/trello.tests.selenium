using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Login
{
    [TestClass]
    public class LoginTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestSuccessfullLogin()
        {
            using var webDriver = Browser.GetChrome();

            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());

            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            bool profileButtonExists = webDriver.CheckIfExists(XPath.DataTestId("header-member-menu-button"));
            Assert.IsTrue(profileButtonExists);

            bool homeLinkExists = webDriver.CheckIfExists(XPath.DataTestId("home-link"));
            Assert.IsTrue(homeLinkExists);
        }
    }
}

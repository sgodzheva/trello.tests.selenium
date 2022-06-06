using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Trello.Tests.Selenium
{
    [TestClass]
    public class LoginTest
    {
        public TestContext TestContext { get; set; }

        private DriverManager driverManager;

        private ChromeDriver webDriver;

        [TestInitialize]
        public void Initialize()
        {
            driverManager = new DriverManager();
            driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            webDriver = new ChromeDriver();
        }

        [TestMethod]
        public void TestSuccessfullLogin()
        {
            TrelloWebApp webApp = new TrelloWebApp(webDriver);
            webApp.Open(TestContext.Properties["webAppUrl"].ToString());

            string username = TestContext.Properties["webAppUserName"].ToString();
            string password = TestContext.Properties["webAppPassword"].ToString();
            webApp.Login(username, password);

            bool profileButtonExists = webDriver.CheckIfExists(XPath.Attribute("data-test-id", "header-member-menu-button"));
            Assert.IsTrue(profileButtonExists);

            bool homeLinkExists = webDriver.CheckIfExists(XPath.Attribute("data-test-id", "home-link"));
            Assert.IsTrue(homeLinkExists);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Thread.Sleep(3000);
            webDriver.Quit();
        }
    }
}

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
    public class LogoutTest
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
        public void TestSuccessfullLogout()
        {
            TrelloWebApp webApp = new TrelloWebApp(webDriver);
            webApp.Open(TestContext.Properties["webAppUrl"].ToString());

            string username = TestContext.Properties["webAppUserName"].ToString();
            string password = TestContext.Properties["webAppPassword"].ToString();
            webApp.Login(username, password);

            webApp.Logout();

            bool loginbuttonExists = webDriver.CheckIfExists(XPath.Attribute("href", "/login"));
            Assert.IsTrue(loginbuttonExists);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Thread.Sleep(3000);
            webDriver.Quit();
        }
    }
}

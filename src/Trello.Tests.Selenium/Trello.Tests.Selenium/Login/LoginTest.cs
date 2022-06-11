using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Trello.Tests.Selenium.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

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

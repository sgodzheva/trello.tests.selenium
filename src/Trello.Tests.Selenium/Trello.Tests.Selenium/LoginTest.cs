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
            webDriver.Navigate().GoToUrl(TestContext.Properties["webAppUrl"].ToString());
            var loginButton = webDriver.FindElement(XPath.Attribute("href", "/login"));
            loginButton.Click();
            var usernameInput = webDriver.FindElement(By.Id("user"));
            usernameInput.SendKeys(TestContext.Properties["webAppUserName"].ToString());

            var atlasianLogin = webDriver.WaitElement(XPath.Attribute("value", "Log in with Atlassian"));
            atlasianLogin.Click();
            var atlasianPassword = webDriver.WaitElement(By.Id("password"));
            atlasianPassword.SendKeys(TestContext.Properties["webAppPassword"].ToString());
            var atlasianLoginButton = webDriver.WaitElement(By.Id("login-submit"));
            atlasianLoginButton.Click();
            Thread.Sleep(3000);
            bool profileButtonExists = webDriver.CheckIfExists(XPath.Attribute("data-test-id", "header-member-menu-button"));
            Assert.IsTrue(profileButtonExists);
            Thread.Sleep(3000);

        }

        [TestCleanup]
        public void Cleanup()
        {
            webDriver.Quit();
        }
    }
}

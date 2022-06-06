﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trello.Tests.Selenium
{
    public class TrelloWebApp
    {
        private IWebDriver webDriver;

        public TrelloWebApp(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void Open(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        public void Login(string username, string password)
        {
            var loginButton = webDriver.FindElement(XPath.Attribute("href", "/login"));
            loginButton.Click();
            var usernameInput = webDriver.FindElement(By.Id("user"));
            usernameInput.SendKeys(username);

            var atlasianLogin = webDriver.WaitElement(XPath.Attribute("value", "Log in with Atlassian"));
            atlasianLogin.Click();
            var atlasianPassword = webDriver.WaitElement(By.Id("password"));
            atlasianPassword.SendKeys(password);
            var atlasianLoginButton = webDriver.WaitElement(By.Id("login-submit"));
            atlasianLoginButton.Click();

            webDriver.WaitElement(XPath.Attribute("data-test-id", "header-member-menu-button"));
        }

        public void Logout()
        {
            var profileButton = webDriver.WaitElement(XPath.Attribute("data-test-id", "header-member-menu-button"));
            profileButton.Click();

            var logoutButton = webDriver.WaitElement(XPath.Attribute("data-test-id", "header-member-menu-logout"));
            logoutButton.Click();
            var atlasianLogout = webDriver.WaitElement(XPath.Attribute("data-testid", "logout-button"));
            atlasianLogout.Click();

            webDriver.WaitElement(XPath.Attribute("href", "/login"));
        }
    }
}

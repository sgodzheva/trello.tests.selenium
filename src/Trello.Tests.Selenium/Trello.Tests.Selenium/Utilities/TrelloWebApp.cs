using OpenQA.Selenium;
using System;
using System.Threading;

namespace Trello.Tests.Selenium.Utilities
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

            if (!webDriver.CheckIfExists(XPath.DataTestId("header-member-menu-button")))
            {
                throw new InvalidOperationException("Login failed");
            }
        }

        public void Logout()
        {
            var profileButton = webDriver.WaitElement(XPath.Attribute("data-test-id", "header-member-menu-button"));
            profileButton.Click();

            var logoutButton = webDriver.WaitElement(XPath.Attribute("data-test-id", "header-member-menu-logout"));
            logoutButton.Click();
            var atlasianLogout = webDriver.WaitElement(XPath.Attribute("data-testid", "logout-button"));
            atlasianLogout.Click();

            if (!webDriver.CheckIfExists(XPath.Attribute("href", "/login")))
            {
                throw new InvalidOperationException("Logout failed");
            }
        }

        public void OpenTestProject()
        {
            var workspaceButton = webDriver.FindElement(XPath.DataTestId("workspace-switcher"));
            workspaceButton.Click();

            var projectButtons = webDriver.WaitElements(XPath.DataTestId("workspace-switcher-popover-tile"));
            foreach (var item in projectButtons)
            {
                var paragraph = item.FindElement(By.TagName("p"));
                if (paragraph.Text == "Testing Project")
                {
                    item.Click();
                }
            }

            webDriver.CheckIfExists(By.ClassName("workspace-boards-page-layout"));

        }

        public void CreateBoard(string boardName)
        {
            var parentElement = webDriver.WaitElement(By.ClassName("workspace-boards-page-layout"));

            var createBoardButton = parentElement.FindElement(XPath.DataTestId("create-board-tile"));
            createBoardButton.Click();

            var boardTitleField = webDriver.WaitElement(XPath.DataTestId("create-board-title-input"));
            boardTitleField.SendKeys(boardName);

            var boardSubmitButton = webDriver.WaitElement(XPath.DataTestId("create-board-submit-button"));
            //Waiting because the button is disabled and we cannot click it.
            Thread.Sleep(1000);
            boardSubmitButton.Click();

            webDriver.CheckIfExists(By.ClassName("js-board-editing-target"));
        }

        public void CreateList(string listName)
        {
            var listField = webDriver.WaitElement(By.ClassName("list-name-input"));
            listField.SendKeys(listName);

            var addListButton = webDriver.FindElement(XPath.Attribute("value", "Add list"));
            addListButton.Click();

            var checkListButton = webDriver.WaitElement(By.ClassName("list-header-name"));
        }
    }
}

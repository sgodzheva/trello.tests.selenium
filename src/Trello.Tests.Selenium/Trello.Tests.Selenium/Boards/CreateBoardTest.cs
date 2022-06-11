using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Boards
{
    [TestClass]
    public class CreateBoardTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestSuccessfullCreationBoard()
        {
            using var webDriver = Browser.GetChrome();

            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());

            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            var workspaceButton = webDriver.FindElement(XPath.DataTestId("workspace-switcher"));
            workspaceButton.Click();

            var projectButton = webDriver.WaitElement(XPath.DataTestId("workspace-switcher-popover-tile"));
            projectButton.Click();

            var parentElement = webDriver.WaitElement(By.ClassName("workspace-boards-page-layout"));

            var createBoardButton = parentElement.FindElement(XPath.DataTestId("create-board-tile"));
            createBoardButton.Click();

            var boardTitleField = webDriver.WaitElement(XPath.DataTestId("create-board-title-input"));
            boardTitleField.SendKeys(TestContext.GetTestName());

            var boardSubmitButton = webDriver.WaitElement(XPath.DataTestId("create-board-submit-button"));
            //Waiting because the button is disabled and we cannot click it.
            Thread.Sleep(1000);
            boardSubmitButton.Click();

            var boardTitleHeader = webDriver.WaitElement(By.ClassName("js-board-editing-target"));

            Assert.AreEqual(TestContext.GetTestName(), boardTitleHeader.Text);
        }
    }
}

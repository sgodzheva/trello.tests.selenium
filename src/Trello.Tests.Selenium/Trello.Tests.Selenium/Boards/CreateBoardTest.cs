﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Boards
{
    [TestClass]
    public class CreateBoardTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var trelloClient = TestContext.GetApiClient();
            trelloClient.DeleteBoardWithName(TestContext.GetTestName());
        }

        [TestMethod]
        public void TestSuccessfullCreationBoard()
        {
            using var webDriver = Browser.GetChrome();

            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());

            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            webApp.OpenTestProject();

            webApp.CreateBoard(TestContext.GetTestName());

            var boardTitleHeader = webDriver.WaitElement(By.ClassName("js-board-editing-target"));

            Assert.AreEqual(TestContext.GetTestName(), boardTitleHeader.Text);
        }
    }
}

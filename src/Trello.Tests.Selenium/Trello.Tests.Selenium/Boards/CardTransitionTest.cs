using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Boards
{
    [TestClass]
    public class CardTransitionTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var trelloclient = TestContext.GetApiClient();
            trelloclient.DeleteBoardWithName(TestContext.GetTestName());
        }

        [TestMethod]
        public void TestSuccessfullCardTransition()
        {
            using var webDriver = Browser.GetChrome();
            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());
            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            webApp.OpenTestProject();
            webApp.CreateBoard(TestContext.GetTestName());

            webApp.CreateList("TO DO");

            var cardButton = webDriver.FindElement(By.ClassName("open-card-composer"));
            cardButton.Click();

            var cardButtonField = webDriver.WaitElement(By.ClassName("list-card-composer-textarea"));
            cardButtonField.Click();

            cardButtonField.SendKeys("@@@@@");
            var addCardButton = webDriver.FindElement(XPath.Attribute("value", "Add card"));
            addCardButton.Click();
            Thread.Sleep(1000);

            var cardElement = webDriver.WaitElement(By.ClassName("list-card-title"));

            webApp.CreateList("In Progress");

            cardElement.Click();

            var moveButton = webDriver.WaitElement(By.ClassName("js-move-card"));
            moveButton.Click();

            var selectListButton = webDriver.WaitElement(By.ClassName("js-select-list"));
            selectListButton.Click();

            var selectElement = new SelectElement(selectListButton);
            selectElement.SelectByText("In Progress");

            var move = webDriver.WaitElement(XPath.Attribute("value", "Move"));
            move.Click();

            var inProgressTitle = webDriver.WaitElement(XPath.Attribute("aria-label", "In Progress"));

            var inProgressList = inProgressTitle.GetParent().GetParent();

            var inProgressCard = inProgressList.FindElement(By.ClassName("list-card-title"));
            var closeButton = webDriver.FindElement(By.ClassName("dialog-close-button"));
            closeButton.Click();

            Assert.IsTrue(inProgressCard.Text == "@@@@@");
            Thread.Sleep(1000);

            inProgressCard.Click();
            var checkCard = webDriver.WaitElement(By.ClassName("js-open-move-from-header"));
            Assert.AreEqual("In Progress", checkCard.Text);

            Thread.Sleep(3000);


        }
    }
}

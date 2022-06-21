using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Boards
{
    [TestClass]
    public class CreateCardTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var trelloclient = TestContext.GetApiClient();
            trelloclient.DeleteBoardWithName(TestContext.GetTestName());
        }

        [TestMethod]
        public void TestSuccessfullCardCreation()
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
            Assert.AreEqual("@@@@@", cardElement.Text);


        }
    }
}

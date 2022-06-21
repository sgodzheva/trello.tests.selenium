using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Boards
{
    [TestClass]
    public class CreateListTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var trelloclient = TestContext.GetApiClient();
            trelloclient.DeleteBoardWithName(TestContext.GetTestName());
        }

        [TestMethod]
        public void TestSuccessfullListCreation()
        {
            using var webDriver = Browser.GetChrome();
            TrelloWebApp webApp = new TrelloWebApp(webDriver);

            webApp.Open(TestContext.GetWebAppUrl());
            webApp.Login(TestContext.GetWebAppUsername(), TestContext.GetWebAppPassword());

            webApp.OpenTestProject();
            webApp.CreateBoard(TestContext.GetTestName());

            webApp.CreateList("TO DO");

            var checkListButton = webDriver.WaitElement(By.ClassName("list-header-name"));

            Assert.AreEqual("TO DO", checkListButton.Text);
        }

    }
}

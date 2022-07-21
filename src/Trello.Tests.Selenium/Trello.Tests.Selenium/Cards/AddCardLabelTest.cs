using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Cards
{
    [TestClass]
    public class AddCardLabelTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var trelloclient = TestContext.GetApiClient();
            trelloclient.DeleteBoardWithName(TestContext.GetTestName());
        }

        [TestMethod]
        public void TestSuccessfullLabelAdding()
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
            cardElement.Click();
            Thread.Sleep(1000);

            var addLabelButton = webDriver.WaitElement(By.ClassName("js-edit-labels"));
            addLabelButton.Click();

            var labelPencilIcon = webDriver.WaitElement(By.ClassName("card-label-edit-button"));
            labelPencilIcon.Click();

            var labelNameField = webDriver.WaitElement(By.Id("labelName"));
            labelNameField.Click();
            labelNameField.SendKeys("NewLabel");

            var labelWindow = webDriver.WaitElement(By.ClassName("edit-label"));

            var labelSaveButton = labelWindow.FindElement(By.ClassName("js-submit"));
            labelSaveButton.Click();

            var labelButton = webDriver.WaitElement(By.ClassName("card-label"));
            labelButton.Click();

            var labelField = webDriver.WaitElement(By.ClassName("card-detail-item-labels"));

            var labelInsideCard = labelField.FindElement(By.ClassName("label-text"));
            Assert.AreEqual("NewLabel", labelInsideCard.Text);

            var closeButton = webDriver.FindElement(By.ClassName("dialog-close-button"));
            closeButton.Click();

            var cardLabels = webDriver.WaitElement(By.ClassName("list-card-labels"));

            var labelWithoutText = cardLabels.FindElement(By.ClassName("card-label"));
            labelWithoutText.Click();

            var labelOnCard = webDriver.WaitElement(By.ClassName("label-text"));
            Assert.AreEqual("NewLabel", labelOnCard.Text);

            Thread.Sleep(1000);
        }
    }
}

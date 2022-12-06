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
            Thread.Sleep(2000);

            var cardElement = webDriver.WaitElement(By.ClassName("list-card-title"));
            cardElement.Click();
            Thread.Sleep(2000);

            var addLabelButton = webDriver.WaitElement(By.ClassName("js-edit-labels"));
            addLabelButton.Click();


            var labelPencilIcon = webDriver.WaitElement(By.XPath("/html/body/div[6]/div/section/div/div/ul/li[1]/label/span[2]/div/button"));
            labelPencilIcon.Click();

            var labelNameField = webDriver.WaitElement(By.XPath("/html/body/div[6]/div/section/div/div[2]/input"));
            labelNameField.Click();
            labelNameField.SendKeys("NewLabel");

            var labelSaveButton = webDriver.WaitElement(By.XPath("/html/body/div[6]/div/section/div/div[4]/button[1]"));
            labelSaveButton.Click();

            var label = webDriver.WaitElement(By.XPath("/html/body/div[6]/div/section/div/div/ul/li[1]/label"));
            label.Click();

            var labelField = webDriver.WaitElement(XPath.DataTestId("card-back-labels-container"));
            var labelButton = labelField.FindElement(XPath.DataTestId("card-label"));
            Assert.AreEqual("NewLabel", labelButton.Text);

            var closeButton = webDriver.FindElement(By.ClassName("dialog-close-button"));
            closeButton.Click();

            var cardLabels = webDriver.WaitElement(By.ClassName("list-card-front-labels-container"));

            var labelWithoutText = cardLabels.FindElement(XPath.DataTestId("compact-card-label"));
            labelWithoutText.Click();

            Assert.AreEqual("NewLabel", labelWithoutText.Text);
        }
    }
}

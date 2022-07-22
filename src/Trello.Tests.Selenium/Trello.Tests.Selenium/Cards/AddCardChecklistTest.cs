using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using Trello.Tests.Selenium.Utilities;

namespace Trello.Tests.Selenium.Cards
{
    [TestClass]
    public class AddCardChecklistTest
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

            var checklistButton = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[3]/div/div/div/div[5]/div[2]/div/a[3]"));
            checklistButton.Click();
            Thread.Sleep(1000);

            var checklistField = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[4]/div/div[2]/div/div/div/form/input[1]"));
            checklistField.Click();
            checklistField.Clear();
            checklistField.SendKeys("Ice cream flavors");
            Thread.Sleep(1000);

            var addChecklistButton = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[4]/div/div[2]/div/div/div/form/input[2]"));
            addChecklistButton.Click();
            Thread.Sleep(1000);

            var addItemButton = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[3]/div/div/div/div[4]/div[10]/div/div[5]/button"));
            if (addItemButton.Displayed)
            {
                addItemButton.Click();
            }

            List<string> flavors = new List<string>() { "vanilla", "chocolate", "yogurt", "mango", "lime" };

            foreach (string flavor in flavors)
            {
                var addItemField = webDriver.WaitElement(By.ClassName("js-new-checklist-item-input"));
                addItemField.SendKeys(flavor);

                var addItem = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[3]/div/div/div/div[4]/div[10]/div/div[5]/div/input"));
                addItem.Click();
            }

            var items = webDriver.FindElements(By.ClassName("checklist-item"));
            Assert.AreEqual(flavors.Count, items.Count);

            foreach (var item in items)
            {
                int position = items.IndexOf(item);

                var checklistItem = item.FindElement(By.ClassName("checklist-item-details-text"));

                Assert.AreEqual(flavors[position], checklistItem.Text);
            }


            var closeButton = webDriver.FindElement(By.ClassName("dialog-close-button"));
            closeButton.Click();

            var checklistOnCard = webDriver.WaitElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div/main/div/div[2]/div[2]/div/div[1]/div[3]/div[2]/div[1]/div/div[2]/a/div[3]/div[2]/span[1]/div/span[2]"));

            Assert.AreEqual($"0/{flavors.Count}", checklistOnCard.Text);
            Thread.Sleep(1000);


        }
    }
}

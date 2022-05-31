using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Trello.Tests.Selenium
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var driverManager = new DriverManager();
            driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            var webDriver = new ChromeDriver();

            webDriver.Navigate().GoToUrl(TestContext.Properties["webAppUrl"].ToString());

            Assert.IsTrue(webDriver.Title.Contains("Trello"));
            Thread.Sleep(10000);
            webDriver.Quit();
        }
    }
}

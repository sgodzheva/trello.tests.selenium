using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Trello.Tests.Selenium.Utilities
{
    public class Browser
    {
        public static ChromeDriver GetChrome()
        {
            var driverManager = new DriverManager();
            driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver();
        }
    }
}

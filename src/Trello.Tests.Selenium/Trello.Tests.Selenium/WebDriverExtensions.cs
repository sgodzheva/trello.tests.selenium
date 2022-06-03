using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trello.Tests.Selenium
{
    public static class WebDriverExtensions
    {
        public static bool CheckIfExists(this WebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static IWebElement WaitElement(this WebDriver driver, By by)
        {
            for (int i = 0; i < 50; i++)
            {
                if (driver.CheckIfExists(by))
                {
                    return driver.FindElement(by);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            return driver.FindElement(by);
        }
    }
}

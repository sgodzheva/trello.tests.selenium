﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Trello.Tests.Selenium.Utilities
{
    public static class WebDriverExtensions
    {
        public static bool CheckIfExists(this IWebDriver driver, By by, int waitTime = 3000)
        {
            int tries = waitTime / 100;
            for (int i = 0; i < tries; i++)
            {

                try
                {
                    var element = driver.FindElement(by);
                    if (element.Displayed)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(100);
                }
            }
            return false;
        }

        public static IWebElement WaitElement(this IWebDriver driver, By by)
        {
            driver.CheckIfExists(by);
            return driver.FindElement(by);
        }

        public static List<IWebElement> WaitElements(this IWebDriver driver, By by)
        {
            driver.CheckIfExists(by);
            return driver.FindElements(by).ToList();
        }

        public static IWebElement GetParent(this IWebElement element)
        {
            return element.FindElement(By.XPath("./.."));
        }


    }
}

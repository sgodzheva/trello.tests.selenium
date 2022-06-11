using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trello.Tests.Selenium.Utilities
{
    public class XPath
    {
        public static By Attribute(string name, string value)
        {
            return By.XPath($"//*[@{name}='{value}']");
        }

        public static By DataTestId(string value)
        {
            return Attribute("data-test-id", value);
        }
    }

}


using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trello.Tests.Selenium
{
    public class XPath
    {
        public static By Attribute(string name, string value)
        {
            return By.XPath($"//*[@{name}='{value}']");
        }
    }
}


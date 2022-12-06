using OpenQA.Selenium;

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
            return Attribute("data-testid", value);
        }
    }

}


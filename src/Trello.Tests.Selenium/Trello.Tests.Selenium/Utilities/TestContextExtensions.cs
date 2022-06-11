using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Trello.Tests.Selenium.Utilities
{
    public static class TestContextExtensions
    {
        public static string GetWebAppUrl(this TestContext testContext)
        {
            return testContext.Properties["webAppUrl"].ToString();
        }

        public static string GetWebAppUsername(this TestContext testContext)
        {
            return testContext.Properties["webAppUserName"].ToString();
        }

        public static string GetWebAppPassword(this TestContext testContext)
        {
            return testContext.Properties["webAppPassword"].ToString();
        }

        public static string GetTestName(this TestContext testContext)
        {
            string[] parts = testContext.FullyQualifiedTestClassName.Split(".");
            var className = parts[parts.Length - 1];
            return $"{className}_{testContext.TestName}";
        }

    }
}

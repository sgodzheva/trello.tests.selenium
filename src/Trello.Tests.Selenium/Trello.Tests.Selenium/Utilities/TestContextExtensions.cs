﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        public static TrelloApiClient GetApiClient(this TestContext testContext)
        {
            string baseUrl = testContext.Properties["apiUrl"].ToString();
            string trelloKey = testContext.Properties["trelloKey"].ToString();
            string trelloToken = testContext.Properties["trelloToken"].ToString();
            string userName = testContext.Properties["userName"].ToString();
            TrelloApiClient trelloClient = new TrelloApiClient(baseUrl, trelloKey, trelloToken, userName);
            return trelloClient;
        }

    }
}

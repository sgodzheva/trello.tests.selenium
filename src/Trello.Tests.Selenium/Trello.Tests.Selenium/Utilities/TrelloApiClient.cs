using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Trello.Tests.Selenium.Utilities.Api;

namespace Trello.Tests.Selenium.Utilities
{
    public class TrelloApiClient
    {
        string baseUrl;
        string trelloKey;
        string trelloToken;
        string userName;
        HttpClient client;

        public TrelloApiClient(string baseUrl, string trelloKey, string trelloToken, string userName)
        {
            this.baseUrl = baseUrl;
            this.trelloKey = trelloKey;
            this.trelloToken = trelloToken;
            this.userName = userName;
            this.client = new HttpClient();
        }

        public List<BoardResult> GetBoards()
        {
            string url = $"{baseUrl}/1/members/{userName}/boards/?key={trelloKey}&token={trelloToken}";
            var response = client.GetAsync(url).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            List<BoardResult> boards = JsonSerializer.Deserialize<List<BoardResult>>(json);
            return boards;
        }

        public void DeleteBoard(string id)
        {
            string urlDelete = $"{baseUrl}/1/boards/{id}?key={trelloKey}&token={trelloToken}";
            var deleteResult = client.DeleteAsync(urlDelete).Result;
        }

        public void DeleteBoardWithName(string boardName)
        {
            List<BoardResult> boards = GetBoards();
            List<BoardResult> boardsToDelete = boards.Where(x => x.name == boardName).ToList();

            foreach (var item in boardsToDelete)
            {
                DeleteBoard(item.id);
                Console.WriteLine($"Deleted board: {item.name}");
            }
        }
    }
}

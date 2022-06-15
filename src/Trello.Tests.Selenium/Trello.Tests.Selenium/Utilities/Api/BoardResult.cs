namespace Trello.Tests.Selenium.Utilities.Api
{
    public class BoardResult
    {
        public string id { get; set; }
        public string name { get; set; }

        public BoardResult(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

namespace Yoma.Core.Infrastructure.Emsi.Models
{
    public class TitleResponse
    {
        public Title[] data { get; set; }
    }

    public class Title
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}

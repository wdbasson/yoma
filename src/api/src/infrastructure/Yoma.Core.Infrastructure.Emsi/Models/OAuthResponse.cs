using Newtonsoft.Json;

namespace Yoma.Core.Infrastructure.Emsi.Models
{
    public class OAuthResponse
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string token_type { get; set; }

        [JsonIgnore]
        public DateTimeOffset Date { get; } = DateTimeOffset.Now;

        [JsonIgnore]
        public DateTimeOffset DateExpire 
        {
            get { return Date.AddSeconds(expires_in - 5); }
        } 
    }
}

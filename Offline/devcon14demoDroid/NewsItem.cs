using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace devcon14demoDroidOffline
{
    public class NewsItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "approved")]
        public bool Approved { get; set; }

        [Version]
        public string Version { get; set; }

        public override string ToString()
        {
            return "Title: " + Title + "\nApproved: " + Approved + "\n";
        }
    }

    public class NewsWrapper : Java.Lang.Object
    {
        public NewsWrapper (NewsItem item)
        {
            News = item;
        }

        public NewsItem News { get; private set; }
    }
}


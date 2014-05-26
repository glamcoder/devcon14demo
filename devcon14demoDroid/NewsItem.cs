using System;
using Newtonsoft.Json;

namespace devcon14demo
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


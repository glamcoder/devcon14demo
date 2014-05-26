using Microsoft.WindowsAzure.Mobile.Service;

namespace devcon14demoService.DataObjects
{
    public class NewsItem : EntityData
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public bool Approved { get; set; }
    }
}
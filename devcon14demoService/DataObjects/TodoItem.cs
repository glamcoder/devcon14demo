using Microsoft.WindowsAzure.Mobile.Service;

namespace devcon14demoService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
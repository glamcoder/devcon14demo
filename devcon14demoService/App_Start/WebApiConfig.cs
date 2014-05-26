using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using devcon14demoService.DataObjects;
using devcon14demoService.Models;

namespace devcon14demoService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new devcon14demoInitializer());
        }
    }

    public class devcon14demoInitializer : DropCreateDatabaseAlways<devcon14demoContext>
    {
        protected override void Seed(devcon14demoContext context)
        {
            List<NewsItem> todoItems = new List<NewsItem>
            {
                new NewsItem { Id = "1", Title = "First news", Text = "Description of first news. Something happened somewhere.", Approved = false },
                new NewsItem { Id = "2", Title = "Second news", Text = "Description of second news. Something didn't happen somewhere.", Approved = true },
            };

            foreach (NewsItem todoItem in todoItems)
            {
                context.Set<NewsItem>().Add(todoItem);
            }

            List<NewsEntry> newitems = new List<NewsEntry>
            {
                new NewsEntry { Id = "1"},
                new NewsEntry { Id = "2"},
            };

            foreach (NewsEntry todoItem in newitems)
            {
                context.Set<NewsEntry>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}


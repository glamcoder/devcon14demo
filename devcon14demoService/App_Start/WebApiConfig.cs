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

    public class devcon14demoInitializer : DropCreateDatabaseIfModelChanges<devcon14demoContext>
    {
        protected override void Seed(devcon14demoContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = "1", Text = "First item", Complete = false },
                new TodoItem { Id = "2", Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}


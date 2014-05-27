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
            var newsItems = new List<NewsItem>
            {
                new NewsItem { Id = "1", Text = "Шокирующие подробности этой новости", Title = "Акулы в Египте, худшие прогнозы",       Approved = false },
                new NewsItem { Id = "2", Text = "Шокирующие подробности этой новости", Title = "Труп осьминога Пауля владеет гипнозом", Approved = false },
                new NewsItem { Id = "3", Text = "Шокирующие подробности этой новости", Title = "Киркоров шокировал Юрмалу юбкой",       Approved = false },
                new NewsItem { Id = "4", Text = "Шокирующие подробности этой новости", Title = "8 таджиков нашли под шлюпкой",          Approved = false },
                new NewsItem { Id = "5", Text = "Шокирующие подробности этой новости", Title = "Голые немки пропали в лесу",            Approved = false },
                new NewsItem { Id = "6", Text = "Шокирующие подробности этой новости", Title = "Канонизировали Алсу",                   Approved = false },
                new NewsItem { Id = "7", Text = "Шокирующие подробности этой новости", Title = "Медвед это клон, а клон — медвед",      Approved = false },
                new NewsItem { Id = "8", Text = "Шокирующие подробности этой новости", Title = "На первом месте в чарте конь-людоед",   Approved = false },
            };

            foreach (NewsItem news in newsItems)
                context.Set<NewsItem>().Add(news);

            base.Seed(context);
        }
    }
}


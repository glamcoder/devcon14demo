using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using devcon14demoService.DataObjects;
using devcon14demoService.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace devcon14demoService.Controllers
{
    public class GetNewsController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GetNews
        [AllowAnonymous]
        public async Task<List<NewsItem>> Get()
        {
            using (var context = new devcon14demoContext())
            {
                var news = await context.NewsItems.Where(n => n.Approved).ToListAsync();
                return news;
            }
        }

    }
}

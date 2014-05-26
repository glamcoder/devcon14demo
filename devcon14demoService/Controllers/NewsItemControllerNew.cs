using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using devcon14demoService.DataObjects;
using devcon14demoService.Models;

namespace devcon14demoService.Controllers
{
    public class NewsItemControllerNew : TableController<NewsItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            devcon14demoContext context = new devcon14demoContext();
            DomainManager = new EntityDomainManager<NewsItem>(context, Request, Services);
        }

        // GET tables/?
        public IQueryable<NewsItem> GetAllNewsItem()
        {
            return Query(); 
        }

        // GET tables/?/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<NewsItem> GetNewsItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/?/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<NewsItem> PatchNewsItem(string id, Delta<NewsItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/?/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostNewsItem(NewsItem item)
        {
            NewsItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/?/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteNewsItem(string id)
        {
             return DeleteAsync(id);
        }

    }
}
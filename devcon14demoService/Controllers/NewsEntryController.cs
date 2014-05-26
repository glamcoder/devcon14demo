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
    public class NewsEntryController : TableController<NewsEntry>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            devcon14demoContext context = new devcon14demoContext();
            DomainManager = new EntityDomainManager<NewsEntry>(context, Request, Services);
        }

        // GET tables/NewsEntry
        [AllowAnonymous]
        public IQueryable<NewsEntry> GetAllNewsEntry()
        {
            return Query(); 
        }

        // GET tables/NewsEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<NewsEntry> GetNewsEntry(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/NewsEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<NewsEntry> PatchNewsEntry(string id, Delta<NewsEntry> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/NewsEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostNewsEntry(NewsEntry item)
        {
            NewsEntry current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/NewsEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteNewsEntry(string id)
        {
             return DeleteAsync(id);
        }

    }
}
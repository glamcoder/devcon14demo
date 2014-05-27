using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using devcon14demoService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Threading.Tasks;

namespace devcon14demoService.Controllers
{
    public class ApproveAllController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/ApproveAll
        public string Get()
        {
            Services.Log.Info("Hello from custom controller!");
            return "Hello";
        }

        public async Task<int> Post()
        {
            try
            {
                using (var context = new devcon14demoContext())
                {
                    var database = context.Database;

                    var sql = @"UPDATE [devcon14demo].[NewsItems] SET Approved = 1 WHERE Approved = 0; SELECT @@ROWCOUNT as count";
                    var result = await database.ExecuteSqlCommandAsync(sql);

                    Services.Log.Info(string.Format("{0} news were approved.", result));

                    return result;
                }
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex.Message);
                throw;
            }
        }

    }
}

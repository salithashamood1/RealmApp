using Microsoft.AspNetCore.Mvc;
using TestRealm.Models;
using TestRealm.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestRealm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileConfigrationController : ControllerBase
    {

        private readonly IAppCreate appCreate;
        public MobileConfigrationController(IAppCreate appCreate)
        {
            this.appCreate = appCreate;
        }
        // GET: api/<MobileConfigrationController>
        /*
        [HttpGet]
        public ActionResult<AppServices> Get()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                return Ok(appCreate.Get("62aab18be59c9c13649e1501", client).Result);
            }
        }

        // GET api/<MobileConfigrationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/<MobileConfigrationController>
        [HttpPost]
        public ActionResult<AppServices> Post([FromBody] AppData app)
        {
            using (var client = new HttpClient())
            {
                var accessToken = appCreate.CreateAccessToken("","",client).Result.access_token;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var appId = appCreate.CreateAppWithName(app.name,client).Result._id;
                if (appId == null)
                {
                    return NotFound("Realm app not created");
                }
                appCreate.CreateAppWithServices(appId, client, app.name);
                appCreate.CreateAppWithAuth(appId, client);
                return Ok(appCreate.Get(appId, client).Result);
            }
        }

        /*
        // PUT api/<MobileConfigrationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MobileConfigrationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}

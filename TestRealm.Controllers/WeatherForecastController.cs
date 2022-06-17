using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TestRealm.Models;

namespace TestRealm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<WeatherForecast>> Get()
        {
            using(var client = new HttpClient())
            {
                // Get access token
                var loginEndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/auth/providers/mongodb-cloud/login");
                var userLogin = new UserLogin()
                {
                    username = "imnboyye",
                    apiKey = "d09e39de-7669-4949-9987-d943e1892fb8"
                };
                var newPostJsonLogin = JsonConvert.SerializeObject(userLogin);
                var payloadLogin = new StringContent(newPostJsonLogin, Encoding.UTF8, "application/json");
                var responseLogin = client.PostAsync(loginEndPoint, payloadLogin).Result;
                var resultLogin = await responseLogin.Content.ReadAsStringAsync();
                var jsonDecodeLogin = JsonConvert.DeserializeObject<UserToken>(resultLogin);
                var accessToken = jsonDecodeLogin.access_token;


                // Create realm app
                client.DefaultRequestHeaders.Authorization =  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var appData = new AppData()
                {
                    name = "testWeb3",
                };

                var createEndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/groups/6290a85d838a146f2c708939/apps");
                var newPostJsonCreate = JsonConvert.SerializeObject(appData);
                var payloadCreate = new StringContent(newPostJsonCreate, Encoding.UTF8, "application/json");
                var responseCreate = client.PostAsync(createEndPoint, payloadCreate).Result;
                var resultCreate = await responseCreate.Content.ReadAsStringAsync();
                var jsonDecodeCreate = JsonConvert.DeserializeObject<AppServices>(resultCreate);
                var appId = jsonDecodeCreate._id;



                // Create app config
                var appConfigEndPoint = "https://realm.mongodb.com/api/admin/v3.0/groups/6290a85d838a146f2c708939/apps/"+ appId + "/services";
                var appConfig = new AppConfig()
                {
                    name = "mongodb-atlas",
                    type = "mongodb-atlas",
                    config = new Config()
                    {
                        clusterName = "Cluster0",
                        readPreference = "primary",
                        wireProtocolEnabled = false,
                        flexible_sync = new FlexibleSync()
                        {
                            state = "enabled",
                            is_recovery_mode_disabled = false,
                            client_max_offline_days = 30,
                            service_name = "mongodb-atlas",
                            database_name = "testWeb3",
                            permissions = new Permissions()
                            {
                                rules = new Rules()
                                {

                                },

                                defaultRoles = {
                                    new DefaultRoles()
                                    {
                                        name = "read-write",
                                        applyWhen = new ApplyWhen()
                                        {

                                        },
                                        read = true,
                                        write = true,
                                    }
                                   }
                            },
                            queryable_fields_names =
                            {
                                "age",
                                "gender",
                                "itemNumber",
                                "mobileNumber",
                                "name"
                            },
                            development_mode_enabled = true
                        }
                    }
                };
                var newPostJsonAppConfig = JsonConvert.SerializeObject(appConfig);
                var payloadAppConfig = new StringContent(newPostJsonAppConfig, Encoding.UTF8, "application/json");
                var responseAppConfig = client.PostAsync(appConfigEndPoint, payloadAppConfig).Result;
                var resultAppConfig = responseAppConfig.Content.ReadAsStringAsync().Result;
                //var resultAppConfig = await responseAppConfig.Content.ReadAsStringAsync();
                //var jsonDecodeAppConfig = JsonConvert.DeserializeObject<AppServices>(resultAppConfig);
                //Console.WriteLine(appConfigEndPoint);


                // Create app auth
                var appAuthConfigEndPoint = "https://realm.mongodb.com/api/admin/v3.0/groups/6290a85d838a146f2c708939/apps/"+appId+"/auth_providers";
                var appAuth = new AppAuth()
                {
                    name = "anon-user",
                    type = "anon-user",
                    disabled = false,
                    config = new AConfig()
                    { }
                };
                var newPostJsonAppAuth = JsonConvert.SerializeObject(appAuth);
                var playloadAppAuth = new StringContent(newPostJsonAppAuth, Encoding.UTF8, "application/json");
                var responseAppAuth = client.PostAsync(appAuthConfigEndPoint, playloadAppAuth).Result;
                var resultAppAuth = responseAppAuth.Content.ReadAsStringAsync().Result;
                Console.Write(resultAppAuth);


            return Ok(resultAppConfig);
            }
        }
    }
}
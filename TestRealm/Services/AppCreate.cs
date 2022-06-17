using Newtonsoft.Json;
using System.Text;
using TestRealm.Models;

namespace TestRealm.Services
{
    public class AppCreate : IAppCreate
    {
        public async Task<string> ConvertToJason(Uri endPoint, string json, HttpClient client)
        {
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(endPoint, payload).Result;
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<UserToken> CreateAccessToken(string userName, string apiKey, HttpClient client)
        {
            try
            {
                var EndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/auth/providers/mongodb-cloud/login");
                var userLogin = new UserLogin()
                {
                    username = "guwxvupz",
                    apiKey = "8883cefb-b217-44e2-b7a3-2315d931e2ac"
                    //username = "imnboyye",
                    //apiKey = "d09e39de-7669-4949-9987-d943e1892fb8"
                    //username = userName,
                    //apiKey = apiKey
                };
                var newPostJson = JsonConvert.SerializeObject(userLogin);
                var result = await ConvertToJason(EndPoint, newPostJson, client);
                var jsonDecode = JsonConvert.DeserializeObject<UserToken>(result);
                return jsonDecode;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<AppServices> CreateAppWithAuth(string id, HttpClient client)
        {
            try
            {
                var EndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/groups/6245db2bc2fda50c40791d24/apps/" + id + "/auth_providers");
                var appAuth = new AppAuth()
                {
                    name = "anon-user",
                    type = "anon-user",
                    disabled = false,
                    config = new AConfig()
                    { }
                };

                var newPostJson = JsonConvert.SerializeObject(appAuth);
                var result = await ConvertToJason(EndPoint, newPostJson, client);
                var jsonDecode = JsonConvert.DeserializeObject<AppServices>(result);
                return jsonDecode;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<AppServices> CreateAppWithName(string name, HttpClient client)
        {
            try
            {
                var EndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/groups/6245db2bc2fda50c40791d24/apps");
                var appData = new AppData()
                {
                    name = name,
                };

                var newPostJson = JsonConvert.SerializeObject(appData);
                var result = await ConvertToJason(EndPoint, newPostJson, client);
                var jsonDecode = JsonConvert.DeserializeObject<AppServices>(result);
                return jsonDecode;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<AppServices> CreateAppWithServices(string id, HttpClient client, string name)
        {
            try
            {
                var EndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/groups/6245db2bc2fda50c40791d24/apps/" + id + "/services");
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
                            database_name = name,
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

                var newPostJson = JsonConvert.SerializeObject(appConfig);
                var result = await ConvertToJason(EndPoint, newPostJson, client);
                var jsonDecode = JsonConvert.DeserializeObject<AppServices>(result);
                return jsonDecode;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<AppServices> Get(string id, HttpClient client)
        {
            try
            {
                var EndPoint = new Uri("https://realm.mongodb.com/api/admin/v3.0/groups/6245db2bc2fda50c40791d24/apps/" + id);

                var response = client.GetAsync(EndPoint).Result;
                var result = await response.Content.ReadAsStringAsync();
                var jsonDecode = JsonConvert.DeserializeObject<AppServices>(result);
                return jsonDecode;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

    }
}

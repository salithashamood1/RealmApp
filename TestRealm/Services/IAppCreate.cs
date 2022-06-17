using TestRealm.Models;

namespace TestRealm.Services
{
    public interface IAppCreate
    {
        Task<AppServices> Get(string id, HttpClient client);
        Task<UserToken> CreateAccessToken(string userName, string apiKey, HttpClient client);
        Task<AppServices> CreateAppWithName(string name, HttpClient client);
        Task<AppServices> CreateAppWithServices(string id, HttpClient client, string name);
        Task<AppServices> CreateAppWithAuth(string id, HttpClient client);
        Task<string> ConvertToJason(Uri endPoint,  String json, HttpClient client);
    }
}

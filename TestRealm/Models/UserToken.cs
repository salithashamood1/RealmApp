namespace TestRealm.Models
{
    public class UserToken
    {
        public String access_token { get; set; } = String.Empty;
        public String refresh_token { get; set; } = String.Empty;
        public String user_id { get; set; } = String.Empty;
        public String device_id { get; set; } = String.Empty;
    }
}

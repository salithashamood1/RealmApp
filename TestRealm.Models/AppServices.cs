namespace TestRealm.Models
{
    public class AppServices
    {
        public string _id { get; set; } = string.Empty;
        public string client_app_id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string provider_region { get; set; } = string.Empty;
        public string deployment_model { get; set; } = string.Empty;
        public string domain_id { get; set; } = string.Empty;
        public string group_id { get; set; } = string.Empty;
        public int last_used { get; set; } = int.MaxValue;
        public int last_modified { get; set; } = int.MaxValue;
        public string product { get; set; } = string.Empty;
        public string environment { get; set; } = string.Empty;
    }
}

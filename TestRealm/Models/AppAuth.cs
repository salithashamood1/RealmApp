namespace TestRealm.Models
{
    public class AppAuth
    {
        public string name { get; set; } = String.Empty;
        public string type { get; set; } = String.Empty;
        public bool disabled { get; set; } = false;
        public AConfig config { get; set; } = new AConfig();
    }

    public class AConfig
    {

    }
}

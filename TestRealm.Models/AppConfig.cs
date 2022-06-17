namespace TestRealm.Models
{
    
    public class AppConfig
    {
        public string name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public Config config { get; set; } = new Config();
    }

    public class Config
    {
        public string clusterName { get; set; } = string.Empty;
        public string readPreference { get; set; } = string.Empty;
        public bool wireProtocolEnabled { get; set; } = false;
        public FlexibleSync flexible_sync { get; set; } = new FlexibleSync();
    }

    public class FlexibleSync
    {
        public string state { get; set; } = string.Empty;
        public bool development_mode_enabled { get; set; } = true;
        public string service_name { get; set; } = string.Empty;
        public int client_max_offline_days { get; set; } = 30;
        public bool is_recovery_mode_disabled { get; set; } = false;
        public string database_name { get; set; } = string.Empty;
        public Permissions permissions { get; set; } = new Permissions();
        public List<string> queryable_fields_names { get; set; } = new List<string>();
    }

    public class Permissions
    {
        public Rules rules { get; set; } = new Rules();
        public List<DefaultRoles> defaultRoles { get; set; } = new List<DefaultRoles>();
    }

    public class Rules
    {

    }

    public class DefaultRoles
    {
        public string name { get; set; } = string.Empty;
        public ApplyWhen applyWhen { get; set; } = new ApplyWhen();
        public bool read { get; set; } = true;
        public bool write { get; set; } = true;
    }

    public class ApplyWhen
    {

    }
}

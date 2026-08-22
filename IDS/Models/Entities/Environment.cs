namespace IDS.Models.Entities
{
    public class Environment
    {
        public int Id { get; set; }
        public int Deployment_id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Purpose { get; set; }
        public string? ServerName { get; set; }
        public string? OperatingSystem { get; set; }
        public string? ApplicationUrl { get; set; }
        public string? DatabaseInfo { get; set; }
        public string? MonitoringLink { get; set; }
        public string? AccessInfo { get; set; }
        public string? Notes { get; set; }
    }
}

namespace IDS.Models.Entities
{
    public class Deployment
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Product_id { get; set; }
        public string? Version { get; set; }
        public DateTime? GoLiveDate { get; set; }
        public string? Status { get; set; }
        public string? SupportTier { get; set; }
        public string? ClientNotes { get; set; }
    }
}

namespace IDS.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Purpose { get; set; }
        public string? Status { get; set; }
        public string? Version { get; set; }
        public string? Markets { get; set; }
        public string? Criticality { get; set; }
        public string? Technologies { get; set; }
        public string? Notes { get; set; }
    }
}

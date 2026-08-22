namespace IDS.Models.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}

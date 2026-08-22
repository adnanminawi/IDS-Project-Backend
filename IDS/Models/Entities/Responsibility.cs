namespace IDS.Models.Entities
{
    public class Responsibility
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Team_id { get; set; }
        public string? Description { get; set; }
    }
}

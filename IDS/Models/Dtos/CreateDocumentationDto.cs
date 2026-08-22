namespace IDS.Models.Dtos
{
    public class CreateDocumentationDto
    {
        public int Product_id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? URL { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
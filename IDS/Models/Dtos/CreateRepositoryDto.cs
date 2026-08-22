namespace IDS.Models.Dtos
{
    public class CreateRepositoryDto
    {
        public int Product_id { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public string? Branch { get; set; }
        public string? Description { get; set; }
    }
}
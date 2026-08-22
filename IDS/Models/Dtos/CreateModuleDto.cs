namespace IDS.Models.Dtos
{
    public class CreateModuleDto
    {
        public int Product_id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
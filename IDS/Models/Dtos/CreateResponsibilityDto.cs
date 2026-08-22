namespace IDS.Models.Dtos
{
    public class CreateResponsibilityDto
    {
        public int Product_id { get; set; }
        public int Team_id { get; set; }
        public string? Description { get; set; }
    }
}
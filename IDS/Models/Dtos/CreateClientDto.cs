namespace IDS.Models.Dtos
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string? Country { get; set; }
        public string? Contact { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
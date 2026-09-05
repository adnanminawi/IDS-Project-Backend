namespace IDS.Models.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string? Position { get; set; }
        public int? TeamId { get; set; }
        public string Role { get; set; }
    }
}
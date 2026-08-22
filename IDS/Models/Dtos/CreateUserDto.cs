namespace IDS.Models.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
namespace IDS.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public int? TeamMember_id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
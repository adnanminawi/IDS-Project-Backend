namespace IDS.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int? TeamMember_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}

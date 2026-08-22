namespace IDS.Models.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public int? Team_id { get; set; }
        public string? RoleInTeam { get; set; }
    }
}

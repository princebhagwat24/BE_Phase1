namespace BE_Phase1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}

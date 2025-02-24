namespace BE_Phase1.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
    }
}
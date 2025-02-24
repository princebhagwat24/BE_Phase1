namespace BE_Phase1.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}

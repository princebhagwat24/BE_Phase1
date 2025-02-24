namespace BE_Phase1.Models
{
    public class Role
    {
        public int RoleId { get; set; } // Add this line for the primary key
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        // Navigation property for the many-to-many relationship
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        // Navigation property for the one-to-many relationship
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

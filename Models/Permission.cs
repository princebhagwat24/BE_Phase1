namespace BE_Phase1.Models
{
    public class Permission
    {
        public int PermissionId { get; set; } // Primary key for Permission
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        // Navigation property for many-to-many relationship with Role
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}

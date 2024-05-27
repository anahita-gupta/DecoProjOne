namespace UserTenantAPI.Models
{
    public class Tenant
    {
        
        public int TenantId { get; set; }
        public string? TenantName { get; set; } // Make TenantName nullable with '?'

        public ICollection<User> Users { get; set; } = new List<User>();
    
    }
}
// Models/User.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTenantAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantKey { get; set; }
        public string? Name { get; set; }

        public Tenant? Tenant { get; set; }

        
    }
}

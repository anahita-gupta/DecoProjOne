using System;
using System.Collections.Generic;
using System.Linq;
using UserTenantAPI.Models;

namespace UserTenantAPI.Data
{
    public class TenantRepository
    {
        private static readonly List<Tenant> _tenants = new List<Tenant>();
        private static readonly object _lock = new object();

        private AppDbContext db = new AppDbContext();
        public TenantRepository() { }

        private static TenantRepository? _instance = null;
        public static TenantRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new TenantRepository();
                }
            }
        }

        public IEnumerable<Tenant> GetAll()
        {
            if (db.TenantItems.Any())
            {
                foreach (var tenant in db.TenantItems)
                {
                    Console.WriteLine($"Retrieving tenant: Id={tenant.TenantId}, Name={tenant.TenantName}");
                }
            }
            else
            {
                Console.WriteLine("No tenants found in the repository.");
            }

            return db.TenantItems;
        }

        public Tenant GetById(int tenantId) => db.TenantItems.FirstOrDefault(t => t.TenantId == tenantId);

        public void Add(Tenant tenant)
        {
            lock (_lock)
            {
                db.TenantItems.Add(tenant);
                db.SaveChanges();
                Console.WriteLine($"Added tenant: Id={tenant.TenantId}, Name={tenant.TenantName}");
            }
        }

        public void Delete(int tenantId)
        {
            lock (_lock)
            {
                var tenant = db.TenantItems.FirstOrDefault(t => t.TenantId == tenantId);
                if (tenant != null)
                {
                    db.TenantItems.Remove(tenant);
                    db.SaveChanges();
                    Console.WriteLine($"Deleted tenant: Id={tenant.TenantId}, Name={tenant.TenantName}");
                }
            }
        }

        public void Update(Tenant tenant)
        {
            lock (_lock)
            {
                var existingTenant = db.TenantItems.FirstOrDefault(t => t.TenantId == tenant.TenantId);
                if (existingTenant != null)
                {
                    existingTenant.TenantName = tenant.TenantName;
                    Console.WriteLine($"Updated tenant: Id={tenant.TenantId}, Name={tenant.TenantName}");
                }
                else
                {
                    Console.WriteLine($"Tenant with Id={tenant.TenantId} not found.");
                }
                db.SaveChanges();
            }
        }
    }
}

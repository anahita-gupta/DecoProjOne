using System;
using System.Collections.Generic;
using System.Linq;
using UserTenantAPI.Models;

namespace UserTenantAPI.Data
{
    public class UserRepository
    {
        private static readonly List<User> _users = new List<User>();
        private static readonly object _lock = new object();
        private AppDbContext db = new AppDbContext();
        public UserRepository() { }

        private static UserRepository? _instance = null;
        public static UserRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new UserRepository();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            if (db.UserItems.Any())
            {
                foreach (var user in db.UserItems)
                {
                    Console.WriteLine($"Retrieving user: Id={user.Id}, TenantKey={user.TenantKey}, Name={user.Name}");
                }
            }
            else
            {
                Console.WriteLine("No users found in the repository.");
            }
            
            return db.UserItems;
        }

        public IEnumerable<User> GetByTenantKey(int tenantKey) => db.UserItems.Where(u => u.TenantKey == tenantKey).ToList();

        public User GetById(int userId) => db.UserItems.FirstOrDefault(t => t.Id == userId);

        


        

        public void Add(User user)
        {
            lock (_lock)
            {
                db.UserItems.Add(user);
                db.SaveChanges();
                Console.WriteLine($"Added user: Id={user.Id}, TenantKey={user.TenantKey}, Name={user.Name}");
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var user = db.UserItems.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.UserItems.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public void Update(User user)
        {
            lock (_lock)
            {
                var existingUser = db.UserItems.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.TenantKey = user.TenantKey;
                    Console.WriteLine($"Updated user: Id={user.Id}, Name={user.Name}, TenantKey={user.TenantKey}");
                }
                else
                {
                    Console.WriteLine($"User with Id={user.Id} not found.");
                }
                db.SaveChanges();
            }

        }
}
}

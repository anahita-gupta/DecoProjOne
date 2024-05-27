using Microsoft.EntityFrameworkCore;
using UserTenantAPI.Models;
using System;
using System.Collections.Generic;

namespace UserTenantAPI.Data{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        // Define the database tables
        public DbSet<Tenant> TenantItems { get; set; }
        public DbSet<User> UserItems { get; set; }

        public string DbPath { get; }

        // Define the database file path
        public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "SQLLiteDatabase.db");
        Console.WriteLine($"Database path: {DbPath}");
    }

        // Configure the database connection
         protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        // Configure the database tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure cascade delete behavior for the User-Tenant relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
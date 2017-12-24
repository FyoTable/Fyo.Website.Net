using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fyo.Models{
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<DeviceSoftware> DeviceSoftwares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceSoftware>().HasKey(ds => new { ds.DeviceId, ds.SoftwareId });
        }
        
    }
}
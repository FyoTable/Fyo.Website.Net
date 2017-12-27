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
        public DbSet<SoftwareVersion> SoftwareVersions { get; set; }
        public DbSet<DeviceSoftwareVersion> DeviceSoftwareVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceSoftwareVersion>().HasKey(ds => new { ds.DeviceId, ds.SoftwareVersionId });
        }
        
    }
}
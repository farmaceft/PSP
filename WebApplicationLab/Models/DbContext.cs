using System.Data.Entity;

namespace WebApplicationLab.Models
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<SystemOperation> SystemOperation { get; set; }
        public DbSet<ScreenSize> ScreenSize { get; set; }
        public DbSet<OZU> OZU { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<Battery> Battery { get; set; }
    }
}
using System.Data.Entity;

namespace WebApplicationLab.Models
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
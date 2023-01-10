using GuData.Models;
using Microsoft.EntityFrameworkCore;

namespace GuAPI.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

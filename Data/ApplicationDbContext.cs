using MessagingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
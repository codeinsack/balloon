using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Balloon.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext() : base("BalloonDb") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}
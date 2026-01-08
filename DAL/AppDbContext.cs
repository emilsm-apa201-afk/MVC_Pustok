using System.Drawing;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.Models;

namespace MVC_Pustokkk.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

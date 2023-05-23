using Microsoft.EntityFrameworkCore;
using ShoppingApi.Configurations;
using ShoppingApi.Models;

namespace ShoppingApi
{
    public class ShoppingDBContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Deliveries> Deliveries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }


        public ShoppingDBContext(DbContextOptions<ShoppingDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoriesConfiguration).Assembly);
        }
    }
}

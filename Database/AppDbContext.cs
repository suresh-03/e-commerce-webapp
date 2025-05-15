using e_commerce_website.Models;
using Microsoft.EntityFrameworkCore;

   

    namespace e_commerce_website.Database
    {
        public class AppDbContext : DbContext
        {
            // Constructor
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            // DbSets for your tables
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<ProductVariant> ProductVariants { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
            public DbSet<Wishlist> Wishlists { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Payment> Payments { get; set; }
        }
    }



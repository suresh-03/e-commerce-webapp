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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            // ****************** ADDING UNIQUE CONSTRAINTS ****************** //

            // Brand Model Configuration
            modelBuilder.Entity<Brand>().HasIndex(b => b.BrandName).IsUnique();


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(c => new { c.UserID, c.VariantID }).IsUnique();
                entity.HasQueryFilter(c => !c.IsDeleted); // Soft delete filter
            });


            modelBuilder.Entity<Category>().HasIndex(c => new { c.CategoryName, c.ParentCategoryID }).IsUnique();

            modelBuilder.Entity<Product>().HasIndex(p => new { p.BrandID, p.ProductName }).IsUnique();

            modelBuilder.Entity<ProductImage>()
            .HasIndex(p => new { p.VariantID, p.IsPrimary })
            .IsUnique()
            .HasFilter("[IsPrimary] = 1"); // This is optional SQL Server syntax to apply it only when true

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                // Composite unique index to prevent duplicate variants per product
                entity.HasIndex(v => new { v.ProductID, v.Size, v.Color }).IsUnique();

                // SKU must be globally unique
                entity.HasIndex(v => v.SKU).IsUnique();
            });

            modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.UserID, r.ProductID })
            .IsUnique();

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Phone).IsUnique();
            });

            modelBuilder.Entity<Wishlist>()
            .HasIndex(w => new { w.UserID, w.VariantID })
            .IsUnique();


            // ******************* CASCADE CONFIGURATIONS ******************* //

            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .HasOne(p => p.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandID)
                    .OnDelete(DeleteBehavior.Restrict); // Don't delete products when brand is deleted

                entity
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryID)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductID)
                .OnDelete(DeleteBehavior.Cascade); // Deleting product deletes its variants


            modelBuilder.Entity<ProductImage>()
                .HasOne(i => i.Variant)
                .WithMany(v => v.Images)
                .HasForeignKey(i => i.VariantID)
                .OnDelete(DeleteBehavior.Cascade); // Deleting variant deletes its images


            modelBuilder.Entity<Cart>(entity =>
            {
                entity
                    .HasOne(c => c.User)
                    .WithMany(u => u.Carts)
                    .HasForeignKey(c => c.UserID)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting user deletes their cart

                entity
                    .HasOne(c => c.Variant)
                    .WithMany(v => v.Carts)
                    .HasForeignKey(c => c.VariantID)
                    .OnDelete(DeleteBehavior.Restrict); // Restrict if variant is in cart
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity
                    .HasOne(w => w.User)
                    .WithMany(u => u.Wishlists)
                    .HasForeignKey(w => w.UserID)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting user deletes wishlist

                entity
                    .HasOne(w => w.Variant)
                    .WithMany(v => v.Wishlists)
                    .HasForeignKey(w => w.VariantID)
                    .OnDelete(DeleteBehavior.Restrict);

            });



            modelBuilder.Entity<Review>(entity =>
            {
                entity
                    .HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserID)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting user deletes their reviews

                entity
                    .HasOne(r => r.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.ProductID)
                    .OnDelete(DeleteBehavior.Cascade); // Deleting product deletes reviews
            });

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent orphan category tree breaking

            }
        }
    }



using AlirezaEShop.Models;
using Microsoft.EntityFrameworkCore;

namespace AlirezaEShop.Data
{
    public class AlirezaEShopContext : DbContext
    {
        public AlirezaEShopContext(DbContextOptions<AlirezaEShopContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.ProductID, t.CategoryID });
              
            modelBuilder.Entity<User>().Property(x=>x.RegisterDate).HasDefaultValueSql("GETDATE()");

            #region Seed Data Category,Item
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                ID = 1,
                Description = "ASP .NET Core",
                Name = "ASP"
            }, new Category()
            {
                ID = 2,
                Description = "ساعت مچی",
                Name = "ساعت مچی"
            },
            new Category()
            {
                ID = 3,
                Description = "لباس ورزشی",
                Name = "گروه لباس ورزشی"
            },
            new Category()
            {
                ID = 4,
                Description = "لوازم منزل",
                Name = "لوازم منزل"
            }
            );

            modelBuilder.Entity<Item>().HasData(new Item()
            {
                Id = 1,
                price = 850.4M,
                quantityInStock = 2,
            },
            new Item()
            {
                Id = 2,
                price = 3302.0M,
                quantityInStock = 5,
            },
            new Item()
            {
                Id = 3,
                price = 2500,
                quantityInStock = 3,
            });

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ID = 1,
                itemID = 1,
                Name = "مبل",
                Description = "مبل خانگی",
                PictureExtention=".jpg"
            }, new Product()
            {
                ID = 2,
                itemID = 2,
                Name = "ساعت",
                Description = "ساعت مچی",
                PictureExtention = ".jpg"

            }, new Product()
            {
                ID = 3,
                itemID = 3,
                Name = "یخچال",
                Description = "یخچال ساید",
                PictureExtention = ".jpg"
            }
            );

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct { CategoryID = 1, ProductID = 1 },
                new CategoryToProduct { CategoryID = 2, ProductID = 1 },
                new CategoryToProduct { CategoryID = 3, ProductID = 1 },
                new CategoryToProduct { CategoryID = 4, ProductID = 1 },
                new CategoryToProduct { CategoryID = 1, ProductID = 2 },
                new CategoryToProduct { CategoryID = 2, ProductID = 2 },
                new CategoryToProduct { CategoryID = 3, ProductID = 2 },
                new CategoryToProduct { CategoryID = 4, ProductID = 2 },
                new CategoryToProduct { CategoryID = 1, ProductID = 3 },
                new CategoryToProduct { CategoryID = 2, ProductID = 3 },
                new CategoryToProduct { CategoryID = 3, ProductID = 3 },
                new CategoryToProduct { CategoryID = 4, ProductID = 3 }
                );

            modelBuilder.Entity<User>().HasData(
                new User { userId = 1, Email = "Alireza@email.com", Password = "12345",  IsAdmin = true }
                );
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}

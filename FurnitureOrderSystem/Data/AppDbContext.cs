using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;
using System.Linq;
using System.Collections.Generic;

namespace FurnitureOrderSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Альтернативный конструктор для конфигурации без DI
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=FurnitureOrders.db")
                    .EnableSensitiveDataLogging()
                    .LogTo(message => System.Diagnostics.Debug.WriteLine(message));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация отношения Order-Customer
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация seed-данных для Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Иван Иванов",
                    Phone = "+79991234567",
                    Email = "ivan@example.com",
                    Address = "ул. Центральная, 10"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Мария Петрова",
                    Phone = "+79998765432",
                    Email = "maria@example.com",
                    Address = "ул. Ленина, 25"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Алексей Сидоров",
                    Phone = "+79997654321",
                    Email = "alex@example.com",
                    Address = "ул. Садовая, 5"
                }
            );

            // Конфигурация seed-данных для Product (пример)
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Диван",
                    Price = 25000.00m,
                    Description = "Удобный трехместный диван"
                },
                new Product
                {
                    Id = 2,
                    Name = "Стул",
                    Price = 3500.00m,
                    Description = "Офисный стул с регулировкой высоты"
                }
            );
        }

        public void InitializeDatabase(bool forceReset = false)
        {
            if (forceReset)
            {
                Database.EnsureDeleted();
            }

            Database.EnsureCreated();

            // Дополнительная проверка и заполнение, если таблицы пустые
            if (!Customers.Any())
            {
                Customers.AddRange(new List<Customer>
                {
                    new Customer
                    {
                        Id = 4,
                        Name = "Елена Васильева",
                        Phone = "+79995554433",
                        Email = "elena@example.com",
                        Address = "ул. Мира, 15"
                    }
                });
            }

            if (!Products.Any())
            {
                Products.AddRange(new List<Product>
                {
                    new Product
                    {
                        Id = 3,
                        Name = "Стол",
                        Price = 12000.00m,
                        Description = "Обеденный стол на 6 персон"
                    }
                });
            }

            SaveChanges();
        }
    }
}
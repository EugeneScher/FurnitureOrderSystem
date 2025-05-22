using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;
using System.Linq;
using System.Collections.Generic;
using System;

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

            modelBuilder.Entity<OrderItem>()
    .HasOne(oi => oi.Order)
    .WithMany(o => o.OrderItems)
    .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Конфигурация seed-данных для Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Иван Иванов",
                    Phone = "+79991234567",
                    Email = "ivan@example.com",
                    Address = "ул. Центральная, 10",
                    RegistrationDate = DateTime.Now.AddDays(-5)
                },
                new Customer
                {
                    Id = 2,
                    Name = "Мария Петрова",
                    Phone = "+79998765432",
                    Email = "maria@example.com",
                    Address = "ул. Ленина, 25",
                    RegistrationDate = DateTime.Now.AddDays(-5)
                },
                new Customer
                {
                    Id = 3,
                    Name = "Алексей Сидоров",
                    Phone = "+79997654321",
                    Email = "alex@example.com",
                    Address = "ул. Садовая, 5",
                    RegistrationDate = DateTime.Now.AddDays(-5)
                }
            );

            // Конфигурация seed-данных для Product (пример)
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Диван",
                    Price = 25000.00m,
                    Description = "Удобный трехместный диван",
                    Category = "Диваны",
                    ImagePath = "Images\\Диван.png"// Добавлено обязательное поле
                },
                new Product
                {
                    Id = 2,
                    Name = "Стул",
                    Price = 3500.00m,
                    Description = "Офисный стул с регулировкой высоты",
                    Category = "Стулья",
                    ImagePath = "Images\\Стул.jpg"// Добавлено обязательное поле
                }
            );
        }

        public void InitializeDatabase(bool forceReset = false)
        {
            try
            {
                if (forceReset)
                {
                    Database.EnsureDeleted();
                }

                // Применяем миграции вместо EnsureCreated()
                Database.Migrate();

                // Проверяем существование таблицы перед запросом
                if (Database.CanConnect())
                {
                    // Добавление клиентов (уже исправлено ранее)
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
                        Address = "ул. Мира, 15",
                        RegistrationDate = DateTime.Now.AddDays(-5)
                    }
                });
                        SaveChanges();
                    }

                    // Исправленный блок для продуктов
                    if (!Products.Any())
                    {
                        Products.AddRange(new List<Product>
                {
                    new Product
                    {
                        Id = 3,
                        Name = "Стол",
                        Price = 12000.00m,
                        Description = "Обеденный стол на 6 персон",
                        Category = "Столы", // Обязательное поле
                        StockQuantity = 8,  // Обязательное поле
                        ImagePath = "Images\\Стол.jpg" // Обязательное поле
                    }
                });
                        SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации БД: {ex.Message}");
            }
        }
    }
    
}
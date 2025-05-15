using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;
using System;

namespace FurnitureOrderSystem.Data
{
    /// <summary>
    /// Контекст базы данных для системы заказов мебели
    /// Объединяет Identity для аутентификации и бизнес-сущности
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSet'ы для всех сущностей системы
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Важно вызывать сначала для Identity

            // Конфигурация отношений между сущностями
            ConfigureRelationships(builder);

            // Заполнение начальными данными
            SeedInitialData(builder);
        }

        /// <summary>
        /// Настройка всех отношений между сущностями
        /// </summary>
        private void ConfigureRelationships(ModelBuilder builder)
        {
            // Заказ -> Клиент
            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Заказ -> Позиции заказа
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Позиция заказа -> Товар
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// Заполнение базы начальными данными
        /// </summary>
        private void SeedInitialData(ModelBuilder builder)
        {
            // Начальные товары
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Диван 'Классик'",
                    Price = 25000,
                    Category = "Диваны",
                    Description = "Классический диван с деревянными ножками",
                    StockQuantity = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "Стол обеденный",
                    Price = 15000,
                    Category = "Столы",
                    Description = "Обеденный стол на 6 персон",
                    StockQuantity = 15
                }
            );

            // Начальные клиенты
            builder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Иванов Иван",
                    Phone = "+79123456789",
                    Email = "ivan@example.com",
                    Address = "ул. Центральная, 1",
                    RegistrationDate = DateTime.Now.AddDays(-30)
                },
                new Customer
                {
                    Id = 2,
                    Name = "Петрова Мария",
                    Phone = "+79098765432",
                    Email = "maria@example.com",
                    Address = "пр. Ленина, 15",
                    RegistrationDate = DateTime.Now.AddDays(-15)
                }
            );
        }
    }
}

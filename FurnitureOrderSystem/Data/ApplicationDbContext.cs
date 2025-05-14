using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;

namespace FurnitureOrderSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Должен быть только ОДИН метод OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=FurnitureOrders.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений и ограничений
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Инициализация начальных данных
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Диван 'Комфорт'", Description = "Удобный трехместный диван", Price = 25000, Category = "Диваны", ImagePath = "sofa.png" },
                new Product { Id = 2, Name = "Стол обеденный", Description = "Деревянный стол на 6 персон", Price = 15000, Category = "Столы", ImagePath = "table.png" },
                new Product { Id = 3, Name = "Шкаф 'Модерн'", Description = "Шкаф с зеркальными дверями", Price = 32000, Category = "Шкафы", ImagePath = "wardrobe.png" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Иванов Иван", Phone = "+79123456789", Email = "ivanov@example.com", Address = "ул. Ленина, 10" }
            );
        }
    }
}
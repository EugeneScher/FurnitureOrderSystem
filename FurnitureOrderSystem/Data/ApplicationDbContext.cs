using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FurnitureOrderSystem.Audit;

namespace FurnitureOrderSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Основные бизнес-сущности
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Конфигурация сущностей
            ConfigureEntities(builder);

            // Настройка отношений
            ConfigureRelationships(builder);

            // Заполнение начальных данных
            SeedInitialData(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
        }

        private void ConfigureEntities(ModelBuilder builder)
        {
            // TODO: Конфигурация сущностей
        }

        private void ConfigureRelationships(ModelBuilder builder)
        {
            // TODO: Настройка отношений
        }

        private void SeedInitialData(ModelBuilder builder)
        {
            // TODO: Заполнение начальных данных
        }
    }
}

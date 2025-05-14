using FurnitureOrderSystem.Data; // Важная директива
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FurnitureOrderSystem
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            InitializeDatabase();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Настройка ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=auth.db"));

            // Остальные сервисы...
        }

        private void InitializeDatabase()
        {
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
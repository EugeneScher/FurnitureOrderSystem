using FurnitureOrderSystem.Services;
using FurnitureOrderSystem.Data;
using FurnitureOrderSystem.Models.ViewModels;
using FurnitureOrderSystem.Views;
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

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Database contexts
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=FurnitureOrders.db"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=auth.db"));

            // Services
            services.AddTransient<IAuthService, AuthService>();

            // ViewModels
            services.AddTransient<MainViewModel>();

            // Views
            services.AddTransient<OrdersView>();
            services.AddSingleton<MainWindow>();
        }

        private void InitializeDatabase()
        {
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var authDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            authDbContext.Database.Migrate();
        }
    }
}

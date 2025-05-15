using FurnitureOrderSystem.Data;
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
            // Настройка основного контекста БД
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=FurnitureOrders.db");
                options.EnableSensitiveDataLogging(); // Для отладки
                options.EnableDetailedErrors(); // Для отладки
            });

            // Настройка контекста для аутентификации (если нужно)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=auth.db"));

            // Регистрация других сервисов приложения...
            // services.AddTransient<IMyService, MyService>();
        }

        private void InitializeDatabase()
        {
            try
            {
                using var scope = ServiceProvider.CreateScope();

                // Инициализация основной БД
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // Применяем миграции
                dbContext.InitializeDatabase(); // Заполняем начальные данные

                // Инициализация БД аутентификации
                var authDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                authDbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации базы данных: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                // Логирование ошибки или другие действия
                Environment.Exit(1);
            }
        }
    }
}
using FurnitureOrderSystem.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FurnitureOrderSystem.Services
{
    public class NavigationService
    {
        public void ShowLoginWindow()
        {
            var window = App.ServiceProvider.GetRequiredService<LoginWindow>();
            window.Show();
            Application.Current.MainWindow = window;
        }

        public void ShowRegisterWindow()
        {
            var window = App.ServiceProvider.GetRequiredService<RegisterWindow>();
            window.Show();
            Application.Current.MainWindow = window;
        }

        public void ShowMainWindow()
        {
            var window = App.ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();
            Application.Current.MainWindow = window;
        }
    }
}
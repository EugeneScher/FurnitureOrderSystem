using FurnitureOrderSystem.Views.Account;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FurnitureOrderSystem.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MainFrame.Content = App.ServiceProvider.GetRequiredService<LoginView>();
        }
    }
}
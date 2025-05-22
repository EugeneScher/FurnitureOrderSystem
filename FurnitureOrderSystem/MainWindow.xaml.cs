using FurnitureOrderSystem.Models.ViewModels;
using FurnitureOrderSystem.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FurnitureOrderSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
            OrdersFrame.Content = App.ServiceProvider.GetRequiredService<OrdersView>();
        }
    }
}

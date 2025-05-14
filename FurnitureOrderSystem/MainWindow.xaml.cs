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

            // Теперь OrdersFrame будет распознаваться
            OrdersFrame.Content = App.ServiceProvider.GetRequiredService<OrdersView>();
        }
    }
}
using FurnitureOrderSystem.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FurnitureOrderSystem.Views
{
    public partial class OrdersView : UserControl // Должен совпадать с XAML
    {
        public OrdersView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<OrderViewModel>();
        }
    }
}
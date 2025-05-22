using FurnitureOrderSystem.Models.Entities;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FurnitureOrderSystem.Data;

namespace FurnitureOrderSystem.Models.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly OrderRepository _orderRepository;

        [ObservableProperty]
        private Order? _order;

        public OrderViewModel(AppDbContext context, int orderId)
        {
            _context = context;
            _orderRepository = new OrderRepository(context);
            LoadOrderAsync(orderId);
        }

        private async void LoadOrderAsync(int orderId)
        {
            Order? order = await _orderRepository.GetOrderWithDetailsByIdAsync(orderId);
            if (order == null) return;

            Order = order;
        }

        [RelayCommand]
        private async Task UpdateStatus(string newStatus)
        {
            if (Order == null) return;

            Order.Status = newStatus;
            await _orderRepository.UpdateAsync(Order);
        }
    }
}
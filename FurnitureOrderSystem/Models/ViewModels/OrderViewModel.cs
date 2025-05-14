using FurnitureOrderSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
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
        private Order _order;

        public OrderViewModel(AppDbContext context, int orderId)
        {
            _context = context;
            _orderRepository = new OrderRepository(context);
            LoadOrderAsync(orderId);
        }

        private async Task LoadOrderAsync(int orderId)
        {
            Order = await _orderRepository.GetOrderWithDetailsByIdAsync(orderId);
        }

        [RelayCommand]
        private async Task UpdateStatus(string newStatus)
        {
            Order.Status = newStatus;
            await _orderRepository.UpdateAsync(Order);
        }
    }
}
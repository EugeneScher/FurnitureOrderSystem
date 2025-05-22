using Microsoft.EntityFrameworkCore;
using FurnitureOrderSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FurnitureOrderSystem.Data
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
        {
            // Явное указание контекста через Set<Order>()
            return await _context.Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsByIdAsync(int id)
        {
            return await _context.Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
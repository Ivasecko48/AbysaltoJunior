using AbySalto.Junior.DTOs;
using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _context;

        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(bool sortByTotal = false)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();

            if (sortByTotal)
                return orders.OrderByDescending(o => o.TotalAmount);

            return orders.OrderByDescending(o => o.OrderTime);
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrder(CreateOrderDTO dto)
        {
            var order = dto.ToModel();
            order.OrderTime = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrderStatus(int id, UpdateOrderStatusDTO dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null) return false;

            order.Status = dto.Status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetOrderTotal(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order?.TotalAmount ?? 0;
        }
    }
}
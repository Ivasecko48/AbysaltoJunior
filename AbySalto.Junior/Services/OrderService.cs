using AbySalto.Junior.Services.DTOs;
using AbySalto.Junior.Services.Common;
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

        public async Task<Result<IEnumerable<Order>>> GetAllOrders(bool sortByTotal = false)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();

            if (sortByTotal)
                return Result<IEnumerable<Order>>.Success(orders.OrderByDescending(o => o.TotalAmount));

            return Result<IEnumerable<Order>>.Success(orders.OrderByDescending(o => o.OrderTime));
        }

        public async Task<Result<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return Result<Order>.Failure(new List<string> { $"Order with id {id} not found." });

            return Result<Order>.Success(order);
        }

        public async Task<Result<Order>> CreateOrder(CreateOrderDTO dto)
        {
            var order = dto.ToModel();
            order.OrderTime = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Result<Order>.Success(order);
        }

        public async Task<Result<bool>> UpdateOrderStatus(int id, UpdateOrderStatusDTO dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null) 
                return Result<bool>.Failure(new List<string> { $"Order with id {id} not found." });

            order.Status = dto.Status;
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<decimal>> GetOrderTotal(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

             if (order is null)
                return Result<decimal>.Failure(new List<string> { $"Order with id {id} not found." });

            return Result<decimal>.Success(order.TotalAmount);
        }
    }
}
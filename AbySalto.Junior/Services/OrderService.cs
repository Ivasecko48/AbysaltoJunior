using AbySalto.Junior.Services.DTOs;
using AbySalto.Junior.Services.Common;
using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AbySalto.Junior.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        
        private const string AllOrdersCacheKey = "all_orders";
        private static readonly DistributedCacheEntryOptions CacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        };

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        };
        public OrderService(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        private ValidationResult ValidateOrder(Order order)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(order.CustomerName))
                result.ValidationItems.Add("Customer name is required.");

            if (string.IsNullOrWhiteSpace(order.DeliveryAddress))
                result.ValidationItems.Add("Delivery address is required.");

            if (string.IsNullOrWhiteSpace(order.ContactNumber))
                result.ValidationItems.Add("Contact number is required.");

            if (order.Items == null || !order.Items.Any())
                result.ValidationItems.Add("Order must have at least one item.");

            foreach (var item in order.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    result.ValidationItems.Add("Item name is required.");
                if (item.Quantity <= 0)
                    result.ValidationItems.Add($"Item quantity '{item.Name}' must have quantity greater than 0.");
                if (item.Price <= 0)
                    result.ValidationItems.Add($"Item price '{item.Name}' must have price greater than 0.");
            }

            return result;
        }

        public async Task<Result<IEnumerable<Order>>> GetAllOrders(bool sortByTotal = false)
        {
            var cached = await _cache.GetStringAsync(AllOrdersCacheKey);

             List<Order> orders;
            if (cached is not null)
            {
                orders = JsonSerializer.Deserialize<List<Order>>(cached, JsonOptions)!;
            }
            else
            {
                orders = await _context.Orders
                    .Include(o => o.Items)
                    .ToListAsync();

                await _cache.SetStringAsync(AllOrdersCacheKey,
                    JsonSerializer.Serialize(orders, JsonOptions), CacheOptions);
            }

            if (sortByTotal)
                return Result<IEnumerable<Order>>.Success(orders.OrderByDescending(o => o.TotalAmount));

            return Result<IEnumerable<Order>>.Success(orders.OrderByDescending(o => o.OrderTime));
        }

        public async Task<Result<Order>> GetOrderById(int id)
        {
            var cacheKey = $"order_{id}";
            var cached = await _cache.GetStringAsync(cacheKey);
            
             Order? order;
            if (cached is not null)
            {
                order = JsonSerializer.Deserialize<Order>(cached, JsonOptions)!;
            }
            else
            {
                order = await _context.Orders
                    .Include(o => o.Items)
                    .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return Result<Order>.Failure(new List<string> { $"Order with id {id} not found." });

            await _cache.SetStringAsync(AllOrdersCacheKey,
                JsonSerializer.Serialize(order, JsonOptions), CacheOptions)!;
            }

            return Result<Order>.Success(order!);
        }

        public async Task<Result<Order>> CreateOrder(CreateOrderDTO dto)
        {
            var order = dto.ToModel();

            var validationResult = ValidateOrder(order);
            if (!validationResult.IsSuccess)
                return Result<Order>.Failure(validationResult.ValidationItems);

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
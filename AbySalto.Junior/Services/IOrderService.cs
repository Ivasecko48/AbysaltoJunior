using AbySalto.Junior.Services.DTOs;
using AbySalto.Junior.Models;

namespace AbySalto.Junior.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders(bool sortByTotal = false);
        Task<Order?> GetOrderById(int id);
        Task<Order> CreateOrder(CreateOrderDTO dto);
        Task<bool> UpdateOrderStatus(int id, UpdateOrderStatusDTO dto);
        Task<decimal> GetOrderTotal(int id);
    }
}
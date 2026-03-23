using AbySalto.Junior.Models;
using AbySalto.Junior.Services.Common;
using AbySalto.Junior.Services.DTOs;

namespace AbySalto.Junior.Services
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetAllOrders(bool sortByTotal = false);
        Task<Result<Order>> GetOrderById(int id);
        Task<Result<Order>> CreateOrder(CreateOrderDTO dto);
        Task<Result<bool>> UpdateOrderStatus(int id, UpdateOrderStatusDTO dto);
        Task<Result<decimal>> GetOrderTotal(int id);
    }
}
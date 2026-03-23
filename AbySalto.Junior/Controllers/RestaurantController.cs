using AbySalto.Junior.Services.DTOs;
using AbySalto.Junior.Models;
using AbySalto.Junior.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IOrderService _orderService;

        public RestaurantController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet("orders")]
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpGet("orders/{id}")]
        public async Task<Order?> GetOrderById(int id)
        {
            return await _orderService.GetOrderById(id);
        }

        [HttpPost("new")]
        public async Task<int> CreateOrder([FromBody] CreateOrderDTO newOrder)
        {
            var created = await _orderService.CreateOrder(newOrder);
            return created.Id;
        }

        [HttpPut("orders/{id}/status")]
        public async Task<bool> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            return await _orderService.UpdateOrderStatus(id, dto);
        }

        [HttpGet("orders/{id}/total")]
        public async Task<decimal> GetOrderTotal(int id)
        {
            return await _orderService.GetOrderTotal(id);
        }

        [HttpGet("orders/sorted")]
        public async Task<IEnumerable<Order>> GetAllOrdersSortedByTotal()
        {
            return await _orderService.GetAllOrders(sortByTotal: true);
        }
    }
}
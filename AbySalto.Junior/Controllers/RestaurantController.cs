using AbySalto.Junior.Services.DTOs;
using AbySalto.Junior.Models;
using AbySalto.Junior.Services;
using AbySalto.Junior.Controllers.Common;
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
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return this.HandleResult(result);
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            return this.HandleResult(result);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO newOrder)
        {
            var result = await _orderService.CreateOrder(newOrder);
            return this.HandleResult(result);
        }

        [HttpPut("orders/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            var result = await _orderService.UpdateOrderStatus(id, dto);
            return this.HandleResult(result);
        }

        [HttpGet("orders/{id}/total")]
        public async Task<IActionResult> GetOrderTotal(int id)
        {
            var result = await _orderService.GetOrderTotal(id);
            return this.HandleResult(result);
        }

        [HttpGet("orders/sorted")]
        public async Task<IActionResult> GetAllOrdersSortedByTotal()
        {
            var result = await _orderService.GetAllOrders(sortByTotal: true);
            return this.HandleResult(result);
        }
    }
}
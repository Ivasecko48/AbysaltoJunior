using AbySalto.Junior.DTOs;
using AbySalto.Junior.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        [HttpGet("orders")]
        public IEnumerable<Order> GetAllOrders()
        {
            // TO DO: dohvati sve narudžbe iz baze
            return Enumerable.Empty<Order>();
        }

        [HttpGet("orders/{id}")]
        public Order GetOrderById([FromRoute] int id)
        {
            // dohvati narudžbu po id-u
            return new Order();
        }

        [HttpPost("new")]
        public int CreateOrder([FromBody] CreateOrderDTO newOrder)
        {
            // spremi novu narudžbu 
            return 0;
        }

        [HttpPatch("orders/{id}/status")]
        public int UpdateOrderStatus([FromRoute] int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            // promijeni status
            return 0;
        }

        [HttpGet("orders/{id}/total")]
        public decimal GetOrderTotal([FromRoute] int id)
        {
            // izračunaj ukupni iznos 
            return 0;
        }
    }
}
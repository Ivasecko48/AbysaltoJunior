using AbySalto.Junior.Models;
using AbySalto.Junior.Models.Enums;

namespace AbySalto.Junior.Services.DTOs
{
    public class CreateOrderDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Note { get; set; }
        public string Currency { get; set; } = "EUR";
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; } = new();

        public Order ToModel()
        {
            return new Order
            {
                CustomerName = CustomerName,
                DeliveryAddress = DeliveryAddress,
                ContactNumber = ContactNumber,
                Note = Note,
                Currency = Currency,
                PaymentMethod = PaymentMethod,
                Items = Items.Select(i => new OrderItem
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
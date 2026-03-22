using AbySalto.Junior.Models.Enums;

namespace AbySalto.Junior.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } =  string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public string? DeliveryAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? Note { get; set; }
        public string Currency { get; set; } = "EUR";
        public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

        #region Reverse Navigation Properties
        public List<OrderItem> Items { get; set; } = new();
        #endregion
    }
}
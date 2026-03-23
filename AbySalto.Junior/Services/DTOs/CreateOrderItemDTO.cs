namespace AbySalto.Junior.Services.DTOs
{
    public class CreateOrderItemDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
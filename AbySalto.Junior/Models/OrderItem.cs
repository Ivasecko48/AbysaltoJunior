namespace AbySalto.Junior.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }

        #region Navigation Properties
        public Order Order { get; set; } = null!;
        #endregion
    }
}
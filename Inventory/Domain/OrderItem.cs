namespace Inventory.Domain
{
    public class OrderItem
    {
        public Order? Order { get; set; }
        public Guid OrderId { get; set; }

        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public double Amount { get; set; }
    }
}

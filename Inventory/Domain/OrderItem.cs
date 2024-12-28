namespace Inventory.Domain
{
    public class OrderItem
    {
        public required string Id { get; set; }

        public Order? Order { get; set; }
        public required string OrderId { get; set; }

        public Asset? Asset { get; set; }
        public required string AssetId { get; set; }

        public double Amount { get; set; }       
    }
}

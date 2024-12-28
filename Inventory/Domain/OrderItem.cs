namespace Inventory.Domain
{
    public class OrderItem
    {
        public string? Id { get; set; }

        public Order? Order { get; set; }
        public string? OrderId { get; set; }

        public Asset? Asset { get; set; }
        public string? AssetId { get; set; }

        public double Amount { get; set; }       
    }
}

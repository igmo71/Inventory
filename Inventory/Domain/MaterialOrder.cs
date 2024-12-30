namespace Inventory.Domain
{
    public class MaterialOrder : Order
    {
        public List<OrderItem>? Items { get; set; }

    }
}

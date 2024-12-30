namespace Inventory.Domain
{
    public class MaterialOrder : Order
    {
        public List<MaterialOrderItem>? OrderItems { get; set; }
    }
}

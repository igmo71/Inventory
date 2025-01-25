namespace Inventory.Domain
{
    public class MaterialOrderItem
    {
        public required string Id { get; set; }

        public MaterialOrder? Order { get; set; }
        public string? OrderId { get; set; }

        public Material? Material { get; set; }
        public string? MayerialId { get; set; }

        public double Count { get; set; }
    }
}

namespace Inventory.Domain
{
    public class Asset
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public bool IsEquipment { get; set; }

        public string? ParentId { get; set; }
        public Asset? Parent { get; set; }

        public List<Asset>? Children { get; set; }
    }
}

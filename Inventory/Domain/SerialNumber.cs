namespace Inventory.Domain
{
    public class SerialNumber
    {
        public string? Id { get; set; }

        public Asset? Asset { get; set; }
        public string? AssetId { get; set; }

        public string? Number { get; set; }
    }
}

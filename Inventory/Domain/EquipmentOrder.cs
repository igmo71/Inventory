namespace Inventory.Domain
{
    public class EquipmentOrder : Order
    {
        public Asset? Asset { get; set; }
        public string? AssetId { get; set; }

        public SerialNumber? SerialNumber { get; set; }
        public string? SerialNumberId { get; set; }
    }
}

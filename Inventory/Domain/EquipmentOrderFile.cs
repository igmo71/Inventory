namespace Inventory.Domain
{
    public class EquipmentOrderFile
    {
        public required string EquipmentOrderId { get; set; }
        public EquipmentOrder? EquipmentOrder { get; set; }
        public required string TrustedFileName { get; set; }
        public string? FileName { get; set; }

    }
}

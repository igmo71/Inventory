namespace Inventory.Domain
{
    public class SerialNumber
    {
        public string? Id { get; set; }

        public Equipment? Equipment { get; set; }
        public string? EquipmentId { get; set; }

        public string? Number { get; set; }
    }
}

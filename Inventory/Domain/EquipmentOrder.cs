namespace Inventory.Domain
{
    public class EquipmentOrder : Order
    {
        public Equipment? Equipment { get; set; }
        public string? EquipmentId { get; set; }

        public SerialNumber? SerialNumber { get; set; }
        public string? SerialNumberId { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}

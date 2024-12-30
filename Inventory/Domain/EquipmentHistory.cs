using Inventory.Data;

namespace Inventory.Domain
{
    public class EquipmentHistory
    {
        public string? Id { get; set; }

        public Equipment? Equipment { get; set; }
        public string? EquipmentId { get; set; }

        public SerialNumber? SerialNumber { get; set; }
        public string? SerialNumberId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public Location? Location { get; set; }
        public Location? LocationId { get; set; }

        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}

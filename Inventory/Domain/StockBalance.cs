using Inventory.Data;

namespace Inventory.Domain
{
    public class StockBalance
    {
        public string? Id { get; set; }
     
        public Asset? Asset { get; set; }
        public string? AssetId { get; set; }

        public SerialNumber? SerialNumber { get; set; }
        public string? SerialNumberId { get; set; }

        public Location? Location { get; set; }
        public string? LocationId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public double Balance { get; set; }       
    }
}

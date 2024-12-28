using Inventory.Data;

namespace Inventory.Domain
{
    public class StockBalance
    {
        public required string Id { get; set; }
     
        public Asset? Asset { get; set; }
        public required string AssetId { get; set; }

        public Location? Location { get; set; }
        public required string LocationId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public required string AssigneeId { get; set; }

        public double Balance { get; set; }       
    }
}

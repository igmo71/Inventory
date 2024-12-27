using Inventory.Data;

namespace Inventory.Domain
{
    public class Assignment
    {
        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public double Balance { get; set; }
    }
}

using Inventory.Data;

namespace Inventory.Domain
{
    public class MaterialBalance
    {
        public string? Id { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public Location? Location { get; set; }
        public Location? LocationId { get; set; }

        public Material? Material { get; set; }
        public string? MaterialId { get; set; }

        public double Balance { get; set; }
    }
}

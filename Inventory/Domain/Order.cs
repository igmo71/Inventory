using Inventory.Data;

namespace Inventory.Domain
{
    public class Order
    {
        public string? Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Number { get; set; }

        public ApplicationUser? Author { get; set; }
        public string? AuthorId { get; set; }

        public bool IsAssigned { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public Location? Location { get; set; }
        public string? LocationId { get; set; }
    }
}

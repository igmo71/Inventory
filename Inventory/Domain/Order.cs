using Inventory.Data;

namespace Inventory.Domain
{
    public class Order
    {
        public required string Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Number { get; set; }

        public ApplicationUser? Author { get; set; }
        public string? AuthorId { get; set; }

        public bool IsAssigned { get; set; }

        public ApplicationUser? AssigneeFrom { get; set; }
        public string? AssigneeFromId { get; set; }

        public ApplicationUser? AssigneeTo { get; set; }
        public string? AssigneeToId { get; set; }

        public Location? LocationFrom { get; set; }
        public string? LocationFromId { get; set; }

        public Location? LocationTo { get; set; }
        public string? LocationToId { get; set; }

        public List<OrderItem>? Items { get; set; }
    }
}

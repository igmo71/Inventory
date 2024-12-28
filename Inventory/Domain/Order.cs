using Inventory.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

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
        public Guid? LocationFromId { get; set; }

        public Location? LocationTo { get; set; }
        public Guid? LocationToId { get; set; }

        public List<OrderItem>? Items { get; set; }
        
        [NotMapped]
        public string? LocationFromIdString
        {
            get
            {
                return LocationFromId.ToString();
            }
            set
            {
                if (value != null)
                    LocationFromId = Guid.Parse(value);
            }
        }

        [NotMapped]
        public string? LocationToIdString
        {
            get
            {
                return LocationToId.ToString();
            }
            set
            {
                if (value != null)
                    LocationToId = Guid.Parse(value);
            }
        }
    }
}

using Inventory.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class Assignment
    {
        public Guid Id { get; set; }

        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public double Balance { get; set; }

        [NotMapped]
        public string? AssetIdString
        {
            get
            {
                return AssetId.ToString();
            }
            set
            {
                if (value != null)
                    AssetId = Guid.Parse(value);
            }
        }
    }
}

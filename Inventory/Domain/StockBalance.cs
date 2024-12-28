using Inventory.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class StockBalance
    {
        public Guid Id { get; set; }
     
        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public Location? Location { get; set; }
        public Guid LocationId { get; set; }

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

        [NotMapped]
        public string? LocationIdString
        {
            get
            {
                return LocationId.ToString();
            }
            set
            {
                if (value != null)
                    LocationId = Guid.Parse(value);
            }
        }
    }
}

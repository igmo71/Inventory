using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Order? Order { get; set; }
        public Guid OrderId { get; set; }

        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public double Amount { get; set; }

        [NotMapped]
        public string? OrderIdString
        {
            get
            {
                return OrderId.ToString();
            }
            set
            {
                if (value != null)
                    OrderId = Guid.Parse(value);
            }
        }

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

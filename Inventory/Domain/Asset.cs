using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class Asset
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Обязательно")]
        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public Guid? ParentId { get; set; }
        public Asset? Parent { get; set; }

        public List<Asset>? Children { get; set; }

        [NotMapped]
        public string? ParentIdString
        {
            get
            {
                return ParentId.ToString();
            }
            set
            {
                if (value != null)
                    ParentId = Guid.Parse(value);
            }
        }
    }
}

namespace Inventory.Domain
{
    public class Location
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Поле 'Наименование' обязательно")]
        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public Guid? ParentId { get; set; }
        public Location? Parent { get; set; }

        public List<Location>? Children { get; set; }

        public List<Asset>? Assets { get; set; }

        //[NotMapped]
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
                else
                    ParentId = null;
            }
        }
    }
}

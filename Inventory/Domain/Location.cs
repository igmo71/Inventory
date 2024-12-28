namespace Inventory.Domain
{
    public class Location
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public string? ParentId { get; set; }
        public Location? Parent { get; set; }

        public List<Location>? Children { get; set; }
    }
}

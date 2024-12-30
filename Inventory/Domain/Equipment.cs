namespace Inventory.Domain
{
    public class Equipment
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public string? ParentId { get; set; }
        public Equipment? Parent { get; set; }

        public List<Equipment>? Children { get; set; }
    }
}

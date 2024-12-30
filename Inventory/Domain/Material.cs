namespace Inventory.Domain
{
    public class Material
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public bool IsFolder { get; set; }

        public string? ParentId { get; set; }
        public Material? Parent { get; set; }

        public List<Material>? Children { get; set; }
    }
}

namespace Inventory.Components.ComponentTreeView
{
    public class TreeItem
    {
        public Guid Id { get; set; }

        public string? Value { get; set; }

        public TreeItem? Parent { get; set; }

        public List<TreeItem>? Children { get; set; }

        public bool HasChildren { get; set; }

        public bool IsFolder { get; set; }

        public bool IsExpanded { get; set; }
    }
}

namespace Inventory.Components.ComponentTreeView
{
    public class TreeItem
    {
        public required string Id { get; set; }

        public string? Value { get; set; }

        public TreeItem? Parent { get; set; }

        public List<TreeItem>? Children { get; set; }

        public bool HasChildren => Children != null && Children.Count > 0;

        public bool IsFolder { get; set; }

        public bool IsExpanded { get; set; }
    }
}

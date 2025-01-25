namespace Inventory.Components.ComponentTreeViewBase
{
    public interface ITreeItem<TItem>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }
        public ITreeItem<TItem>? Parent { get; set; }

        public List<ITreeItem<TItem>>? Children { get; set; }

        public bool IsExpanded { get; set; }
    }
}

using Inventory.Components.ComponentTreeView;

namespace Inventory.Components.ComponentTreeViewBase
{
    public class TreeBuilder
    {
        public static List<TreeItem> Build(List<TreeItem> items)
        {
            var rootNodes = new List<TreeItem>();

            var lookup = items.ToDictionary(k => k.Id!, v => v);

            return rootNodes;
        }
    }
}

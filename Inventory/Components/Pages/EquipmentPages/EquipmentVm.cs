using Inventory.Domain;

namespace Inventory.Components.Pages.EquipmentPages
{
    public class EquipmentVm : Equipment
    {
        public EquipmentVm()
        { }

        public EquipmentVm(Equipment equipment)
        {
            Id = equipment.Id;
            Name = equipment.Name;
            IsFolder = equipment.IsFolder;
            ParentId = equipment.ParentId;
            Parent = equipment.Parent;
            Children = equipment.Children;
            Expanded = false;
        }

        public bool Expanded { get; set; }
    }
}

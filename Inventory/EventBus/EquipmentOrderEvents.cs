using Inventory.Domain;

namespace Inventory.EventBus
{
    public interface IEquipmentOrderEvent
    { }

    public record EquipmentOrderCreatedEvent(EquipmentOrder EquipmentOrder) : IEquipmentOrderEvent;
    public record EquipmentOrderUpdatedEvent(EquipmentOrder EquipmentOrder) : IEquipmentOrderEvent;
    public record EquipmentOrderDeletedEvent(EquipmentOrder EquipmentOrder) : IEquipmentOrderEvent;
}

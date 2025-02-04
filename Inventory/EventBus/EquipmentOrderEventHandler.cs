using Inventory.Application;
using Inventory.Domain;

namespace Inventory.EventBus
{
    public interface IEquipmentOrderEventHandler
    { }

    public class EquipmentOrderEventHandler : IEquipmentOrderEventHandler, IDisposable
    {
        private readonly ISerialNumberService _serialNumberService;

        public EquipmentOrderEventHandler(ISerialNumberService serialNumberService)
        {
            _serialNumberService = serialNumberService;

            EquipmentOrderEventDispatcher.EquipmentOrderCreated +=
                async (sender, equipmentOrderCreatedEvent) => await EquipmentOrderCreatedHandle(equipmentOrderCreatedEvent);

            EquipmentOrderEventDispatcher.EquipmentOrderUpdated +=
                async (sender, equipmentOrderUpdatedEvent) => await EquipmentOrderUpdatedHandle(equipmentOrderUpdatedEvent);

            EquipmentOrderEventDispatcher.EquipmentOrderDeleted +=
                async (sender, equipmentOrderDeletedEvent) => await EquipmentOrderDeletedHandle(equipmentOrderDeletedEvent);
        }

        public void Dispose()
        {
            EquipmentOrderEventDispatcher.EquipmentOrderCreated -=
                async (sender, equipmentOrderCreatedEvent) => await EquipmentOrderCreatedHandle(equipmentOrderCreatedEvent);

            EquipmentOrderEventDispatcher.EquipmentOrderUpdated -=
                async (sender, equipmentOrderUpdatedEvent) => await EquipmentOrderUpdatedHandle(equipmentOrderUpdatedEvent);

            EquipmentOrderEventDispatcher.EquipmentOrderDeleted -=
                async (sender, equipmentOrderDeletedEvent) => await EquipmentOrderDeletedHandle(equipmentOrderDeletedEvent);
        }

        private async Task EquipmentOrderCreatedHandle(EquipmentOrderCreatedEvent equipmentOrderCreatedEvent)
        {
            await _serialNumberService.Assign(equipmentOrderCreatedEvent.EquipmentOrder.SerialNumberId);
        }

        private async Task EquipmentOrderUpdatedHandle(EquipmentOrderUpdatedEvent equipmentOrderUpdatedEvent)
        {
            if (equipmentOrderUpdatedEvent.EquipmentOrder.ReturnDate is null)
                await _serialNumberService.Assign(equipmentOrderUpdatedEvent.EquipmentOrder.SerialNumberId);
            else
                await _serialNumberService.UnAssign(equipmentOrderUpdatedEvent.EquipmentOrder.SerialNumberId);
        }

        private async Task EquipmentOrderDeletedHandle(EquipmentOrderDeletedEvent equipmentOrderDeletedEvent)
        {
            await _serialNumberService.UnAssign(equipmentOrderDeletedEvent.EquipmentOrder.SerialNumberId);
        }
    }
}

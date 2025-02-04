namespace Inventory.EventBus
{
    public class EquipmentOrderEventDispatcher : BackgroundService
    {
        private readonly IEquipmentOrderEventBus _equipmentOrderEventBus;

        public EquipmentOrderEventDispatcher(IEquipmentOrderEventBus equipmentOrderEventBus)
        {
            _equipmentOrderEventBus = equipmentOrderEventBus;
        }

        public static event EventHandler<EquipmentOrderCreatedEvent>? EquipmentOrderCreated;
        public static event EventHandler<EquipmentOrderUpdatedEvent>? EquipmentOrderUpdated;
        public static event EventHandler<EquipmentOrderDeletedEvent>? EquipmentOrderDeleted;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var equipmentOrderEvent in _equipmentOrderEventBus.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    switch (equipmentOrderEvent)
                    {
                        case EquipmentOrderCreatedEvent equipmentOrderCreatedEvent:
                            OnEquipmentOrderCreated(equipmentOrderCreatedEvent);
                            break;
                        case EquipmentOrderUpdatedEvent equipmentOrderUpdatedEvent:
                            OnEquipmentOrderUpdated(equipmentOrderUpdatedEvent);
                            break;
                        case EquipmentOrderDeletedEvent equipmentOrderDeletedEvent:
                            OnEquipmentOrderDeleted(equipmentOrderDeletedEvent);
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void OnEquipmentOrderCreated(EquipmentOrderCreatedEvent equipmentOrderCreatedEvent)
        {
            EventHandler<EquipmentOrderCreatedEvent>? equipmentOrderCreatedHandler = EquipmentOrderCreated;
            equipmentOrderCreatedHandler?.Invoke(this, equipmentOrderCreatedEvent);
        }

        private void OnEquipmentOrderUpdated(EquipmentOrderUpdatedEvent equipmentOrderUpdatedEvent)
        {
            EventHandler<EquipmentOrderUpdatedEvent>? equipmentOrderUpdatedHandler = EquipmentOrderUpdated;
            equipmentOrderUpdatedHandler?.Invoke(this, equipmentOrderUpdatedEvent);
        }

        private void OnEquipmentOrderDeleted(EquipmentOrderDeletedEvent equipmentOrderDeletedEvent)
        {
            EventHandler<EquipmentOrderDeletedEvent>? equipmentOrderDeletedHandler = EquipmentOrderDeleted;
            equipmentOrderDeletedHandler?.Invoke(this, equipmentOrderDeletedEvent);
        }
    }
}

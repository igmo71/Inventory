using Inventory.Domain;
using System.Threading.Channels;

namespace Inventory.EventBus
{
    public interface IEquipmentOrderEventBus
    {
        ChannelReader<IEquipmentOrderEvent> Reader {  get; }

        Task PublishEquipmentOrderCreated(EquipmentOrder equipmentOrder);
        Task PublishEquipmentOrderUpdated(EquipmentOrder equipmentOrder);
        Task PublishEquipmentOrderDeleted(EquipmentOrder equipmentOrder);
    }

    public class EquipmentOrderEventBus : IEquipmentOrderEventBus
    {
        private readonly Channel<IEquipmentOrderEvent> _channel = Channel.CreateUnbounded<IEquipmentOrderEvent>();
        public ChannelWriter<IEquipmentOrderEvent> Writer => _channel.Writer;
        public ChannelReader<IEquipmentOrderEvent> Reader => _channel.Reader;

        public async Task PublishEquipmentOrderCreated(EquipmentOrder equipmentOrder)
        {
            await Writer.WriteAsync(new EquipmentOrderCreatedEvent(equipmentOrder));
        }

        public async Task PublishEquipmentOrderUpdated(EquipmentOrder equipmentOrder)
        {
            await Writer.WriteAsync(new EquipmentOrderUpdatedEvent(equipmentOrder));
        }

        public async Task PublishEquipmentOrderDeleted(EquipmentOrder equipmentOrder)
        {
            await Writer.WriteAsync(new EquipmentOrderDeletedEvent(equipmentOrder));
        }
    }
}

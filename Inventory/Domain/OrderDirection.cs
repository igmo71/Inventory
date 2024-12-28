namespace Inventory.Domain
{
    public record OrderDirection (int Id, string Name, string NameRu)
    {
        public static OrderDirection Inbound { get; } = new OrderDirection(1, "Inbound", "Приход");
        public static OrderDirection Outbound { get; } = new OrderDirection(2, "Outbound", "Расход");
        public static OrderDirection Transfer { get; } = new OrderDirection(3, "Transfer", "Перемещение");
    }
}

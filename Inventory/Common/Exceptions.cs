namespace Inventory.Common
{
    public class NotFoundException(string? entity, string? id) : Exception($"{entity} with {id} not found");
}

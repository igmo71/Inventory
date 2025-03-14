namespace Inventory.Domain
{
    public class AppFile
    {
        public required string Id { get; set; }
        public required string TrustedFileName { get; set; }
        public string? FileName { get; set; }
    }
}

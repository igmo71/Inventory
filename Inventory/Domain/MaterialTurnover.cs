using Inventory.Data;

namespace Inventory.Domain
{
    public class MaterialTurnover
    {
        public required string Id { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public Location? Location { get; set; }
        public string? LocationId { get; set; }

        public Material? Material { get; set; }
        public string? MaterialId { get; set; }

        public MaterialOrder? Order { get; set; }
        public string? OrderId { get; set; }

        public DateTime? DateTime { get; set; }

        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expense { get; set; }
        public double ClosingBalance { get; set; }
        public double Turnover => Receipt - Expense;
    }
}

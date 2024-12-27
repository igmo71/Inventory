using Inventory.Data;

namespace Inventory.Domain
{
    public class AssignTurnover
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public Order? Order { get; set; }
        public Guid OrderId { get; set; }

        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expense { get; set; }
        public double ClosingBalance { get; set; }
        public double Turnover => Receipt - Expense;
    }
}

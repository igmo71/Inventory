using Inventory.Data;

namespace Inventory.Domain
{
    public class StockTurnover
    {
        public required string Id { get; set; }

        public DateTime DateTime { get; set; }

        public Order? Order { get; set; }
        public required string OrderId { get; set; }

        public Asset? Asset { get; set; }
        public required string AssetId { get; set; }

        public Location? Location { get; set; }
        public required string LocationId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public required string AssigneeId { get; set; }

        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expense { get; set; }
        public double ClosingBalance { get; set; }
        public double Turnover => Receipt - Expense;       
    }
}

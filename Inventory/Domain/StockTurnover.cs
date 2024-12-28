using Inventory.Data;

namespace Inventory.Domain
{
    public class StockTurnover
    {
        public string? Id { get; set; }

        public DateTime DateTime { get; set; }

        public Order? Order { get; set; }
        public string? OrderId { get; set; }

        public Asset? Asset { get; set; }
        public string? AssetId { get; set; }

        public SerialNumber? SerialNumber { get; set; }
        public string? SerialNumberId { get; set; }

        public Location? Location { get; set; }
        public string? LocationId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expense { get; set; }
        public double ClosingBalance { get; set; }
        public double Turnover => Receipt - Expense;       
    }
}

namespace Inventory.Domain
{
    public class StockTurnover
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public Order? Order { get; set; }
        public Guid OrderId { get; set; }

        public Asset? Asset { get; set; }
        public Guid AssetId { get; set; }

        public Location? Location { get; set; }
        public Guid LocationId { get; set; }

        public double OpeningBalance { get; set; }
        public double Receipt { get; set; }
        public double Expense { get; set; }
        public double ClosingBalance { get; set; }
        public double Turnover => Receipt - Expense;

        //[NotMapped]
        public string? OrderIdString
        {
            get
            {
                return OrderId.ToString();
            }
            set
            {
                if (value != null)
                    OrderId = Guid.Parse(value);
            }
        }

        //[NotMapped]
        public string? AssetIdString
        {
            get
            {
                return AssetId.ToString();
            }
            set
            {
                if (value != null)
                    AssetId = Guid.Parse(value);
            }
        }

        //[NotMapped]
        public string? LocationIdString
        {
            get
            {
                return LocationId.ToString();
            }
            set
            {
                if (value != null)
                    LocationId = Guid.Parse(value);
            }
        }
    }
}

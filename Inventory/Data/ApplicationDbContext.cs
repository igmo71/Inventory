using Inventory.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Asset> Assets { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDirection> OrderDirections { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<AssignTurnover> AssignTurnovers { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockTurnover> StockTurnovers { get; set; }
    }
}

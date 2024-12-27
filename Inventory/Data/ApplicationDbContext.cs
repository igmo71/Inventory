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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Asset>().HasKey(x => x.Id);
            builder.Entity<Asset>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Asset>().HasMany(e => e.Locations).WithMany(e => e.Assets).UsingEntity<Stock>(
                    r => r.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade)
            );

            builder.Entity<Location>().HasKey(x => x.Id);
            builder.Entity<Location>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Location>().HasMany(e => e.Assets).WithMany(e => e.Locations).UsingEntity<Stock>(
                    l => l.HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade)
            );

            builder.Entity<Order>().HasKey(x => x.Id);
            builder.Entity<Order>().HasOne(e => e.Direction).WithMany().HasForeignKey(e => e.DirectionId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Status).WithMany().HasForeignKey(e => e.StatusId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.LocationFrom).WithMany().HasForeignKey(e => e.LocationFromId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.LocationTo).WithMany().HasForeignKey(e => e.LocationToId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasMany(e => e.Items).WithOne(e => e.Order).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>().HasKey(e => e.Id);
            builder.Entity<OrderItem>().HasOne(e => e.Order).WithMany(e => e.Items).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Assignment>().HasKey(e => new { e.AssetId, e.AssigneeId });
            builder.Entity<Assignment>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Assignment>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AssignTurnover>().HasKey(e => e.Id);
            builder.Entity<AssignTurnover>().HasOne(e => e.Order).WithMany().HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AssignTurnover>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AssignTurnover>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Stock>().HasKey(e => new { e.AssetId, e.LocationId });
            builder.Entity<Stock>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Stock>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StockTurnover>().HasKey(e => e.Id);
            builder.Entity<StockTurnover>().HasOne(e => e.Order).WithMany().HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

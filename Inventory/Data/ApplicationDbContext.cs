using Inventory.Common;
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
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<StockBalance> StockBalances { get; set; }
        public DbSet<StockTurnover> StockTurnovers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Asset>().HasKey(e => e.Id);
            builder.Entity<Asset>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Asset>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Asset>().Property(e => e.ParentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Asset>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);
            builder.Entity<Asset>().Property(e => e.SerialNumber).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<Location>().HasKey(e => e.Id);
            builder.Entity<Location>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Location>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Location>().Property(e => e.ParentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Location>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<Order>().HasKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.AssigneeFrom).WithMany().HasForeignKey(e => e.AssigneeFromId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.AssigneeTo).WithMany().HasForeignKey(e => e.AssigneeToId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.LocationFrom).WithMany().HasForeignKey(e => e.LocationFromId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.LocationTo).WithMany().HasForeignKey(e => e.LocationToId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasMany(e => e.Items).WithOne(e => e.Order).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Order>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.AuthorId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.AssigneeFromId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.AssigneeToId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.LocationFromId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.LocationToId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.Number).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<OrderItem>().HasKey(e => e.Id);
            builder.Entity<OrderItem>().HasOne(e => e.Order).WithMany(e => e.Items).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<OrderItem>().Property(e => e.OrderId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<OrderItem>().Property(e => e.AssetId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<StockBalance>().HasKey(e => e.Id);
            builder.Entity<StockBalance>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockBalance>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockBalance>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockBalance>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockBalance>().Property(e => e.AssetId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockBalance>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockBalance>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<StockTurnover>().HasKey(e => e.Id);
            builder.Entity<StockTurnover>().HasOne(e => e.Order).WithMany().HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().HasOne(e => e.Asset).WithMany().HasForeignKey(e => e.AssetId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockTurnover>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockTurnover>().Property(e => e.OrderId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockTurnover>().Property(e => e.AssetId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockTurnover>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<StockTurnover>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.GUID_LENGTH);
        }
    }
}

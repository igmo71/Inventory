using Inventory.Common;
using Inventory.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistories { get; set; }
        public DbSet<EquipmentOrder> EquipmentOrders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialBalance> MaterialBalances { get; set; }
        public DbSet<MaterialOrder> MaterialOrders { get; set; }
        public DbSet<MaterialOrderItem> MaterialOrderItems { get; set; }
        public DbSet<MaterialTurnover> MaterialTurnovers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<Equipment>().HasKey(e => e.Id);
            builder.Entity<Equipment>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Equipment>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Equipment>().Property(e => e.ParentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Equipment>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<EquipmentHistory>().HasKey(e => e.Id);
            builder.Entity<EquipmentHistory>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentHistory>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentHistory>().HasOne(e => e.Equipment).WithMany().HasForeignKey(e => e.EquipmentId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentHistory>().HasOne(e => e.SerialNumber).WithMany().HasForeignKey(e => e.SerialNumberId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentHistory>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<EquipmentHistory>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.USER_ID_LENGTH);
            builder.Entity<EquipmentHistory>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<EquipmentHistory>().Property(e => e.EquipmentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<EquipmentHistory>().Property(e => e.SerialNumberId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<EquipmentOrder>().HasOne(e => e.Equipment).WithMany().HasForeignKey(e => e.EquipmentId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentOrder>().HasOne(e => e.SerialNumber).WithMany().HasForeignKey(e => e.SerialNumberId).HasPrincipalKey(e => e.Id);
            builder.Entity<EquipmentOrder>().Property(e => e.EquipmentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<EquipmentOrder>().Property(e => e.SerialNumberId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<Location>().HasKey(e => e.Id);
            builder.Entity<Location>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Location>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Location>().Property(e => e.ParentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Location>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<Material>().HasKey(e => e.Id);
            builder.Entity<Material>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).HasPrincipalKey(e => e.Id);
            builder.Entity<Material>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Material>().Property(e => e.ParentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Material>().Property(e => e.Name).HasMaxLength(AppSettings.NAME_LENGTH);

            builder.Entity<MaterialBalance>().HasKey(e => e.Id);
            builder.Entity<MaterialBalance>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialBalance>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialBalance>().HasOne(e => e.Material).WithMany().HasForeignKey(e => e.MaterialId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialBalance>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialBalance>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.USER_ID_LENGTH);
            builder.Entity<MaterialBalance>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialBalance>().Property(e => e.MaterialId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<MaterialOrder>().HasMany(e => e.OrderItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MaterialOrderItem>().HasKey(e => e.Id);
            builder.Entity<MaterialOrderItem>().HasOne(e => e.Order).WithMany(e => e.OrderItems).HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialOrderItem>().HasOne(e => e.Material).WithMany().HasForeignKey(e => e.MayerialId).HasForeignKey(e => e.MayerialId).HasPrincipalKey(e => e.Id);
            builder.Entity<MaterialOrderItem>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialOrderItem>().Property(e => e.OrderId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialOrderItem>().Property(e => e.MayerialId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<MaterialTurnover>().HasKey(e => e.Id);
            builder.Entity<MaterialTurnover>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialTurnover>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialTurnover>().HasOne(e => e.Material).WithMany().HasForeignKey(e => e.MaterialId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialTurnover>().HasOne(e => e.Order).WithMany().HasForeignKey(e => e.OrderId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MaterialTurnover>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialTurnover>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.USER_ID_LENGTH);
            builder.Entity<MaterialTurnover>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialTurnover>().Property(e => e.MaterialId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<MaterialTurnover>().Property(e => e.OrderId).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<Order>().HasKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Assignee).WithMany().HasForeignKey(e => e.AssigneeId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id);
            builder.Entity<Order>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.AuthorId).HasMaxLength(AppSettings.USER_ID_LENGTH);
            builder.Entity<Order>().Property(e => e.AssigneeId).HasMaxLength(AppSettings.USER_ID_LENGTH);
            builder.Entity<Order>().Property(e => e.LocationId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<Order>().Property(e => e.Number).HasMaxLength(AppSettings.GUID_LENGTH);

            builder.Entity<SerialNumber>().HasKey(e => e.Id);
            builder.Entity<SerialNumber>().HasOne(e => e.Equipment).WithMany().HasForeignKey(e => e.EquipmentId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SerialNumber>().Property(e => e.Id).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<SerialNumber>().Property(e => e.EquipmentId).HasMaxLength(AppSettings.GUID_LENGTH);
            builder.Entity<SerialNumber>().Property(e => e.Number).HasMaxLength(AppSettings.GUID_LENGTH);
        }
    }
}

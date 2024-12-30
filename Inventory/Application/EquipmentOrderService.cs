using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IEquipmentOrderService
    {
        Task<ListResult<EquipmentOrder>> GetList(int skip, int? take);
        Task<EquipmentOrder?> Get(string id);
        Task<string> Create(EquipmentOrder equipmentOrder);
        Task<Result> Update(EquipmentOrder equipmentOrder);
        Task Delete(EquipmentOrder equipmentOrder);
        bool Exists(string id);
    }

    public class EquipmentOrderService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEquipmentOrderService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<EquipmentOrder>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.EquipmentOrders.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.EquipmentOrders.Count();

            return ListResult<EquipmentOrder>.Success(result, total);
        }

        public async Task<EquipmentOrder?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var equipmentOrder = await context.EquipmentOrders.FirstOrDefaultAsync(m => m.Id == id);
            return equipmentOrder;
        }

        public async Task<string> Create(EquipmentOrder equipmentOrder)
        {
            using var context = _dbFactory.CreateDbContext();
            equipmentOrder.Id = Guid.CreateVersion7().ToString();
            context.EquipmentOrders.Add(equipmentOrder);
            await context.SaveChangesAsync();
            return equipmentOrder.Id;
        }

        public async Task<Result> Update(EquipmentOrder equipmentOrder)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(equipmentOrder).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(equipmentOrder.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(EquipmentOrder), equipmentOrder.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(EquipmentOrder equipmentOrder)
        {
            using var context = _dbFactory.CreateDbContext();
            context.EquipmentOrders.Remove(equipmentOrder);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.EquipmentOrders.Any(e => e.Id == id);
        }
    }
}

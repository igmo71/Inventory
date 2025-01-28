using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.EquipmentOrderServices
{
    public interface IEquipmentOrderService
    {
        Task<ListResult<EquipmentOrder>> GetList(
            GridItemsProviderRequest<EquipmentOrder> request,
            EquipmentOrderIncludeParameters includeParameters,
            EquipmentOrderFilterParameters filterParameters);

        Task<EquipmentOrder?> Get(
            string id, 
            EquipmentOrderIncludeParameters includeParameters);

        Task<string> Create(EquipmentOrder equipmentOrder);
        Task<Result> Update(EquipmentOrder equipmentOrder);
        Task Delete(EquipmentOrder equipmentOrder);
        bool Exists(string id);
    }

    public class EquipmentOrderService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEquipmentOrderService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<EquipmentOrder>> GetList(
            GridItemsProviderRequest<EquipmentOrder> request,
            EquipmentOrderIncludeParameters includeParameters,
            EquipmentOrderFilterParameters filterParameters)
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.EquipmentOrders
                .AsNoTracking()
                .HandleRequest(request)
                .PerformInclude(includeParameters)
                .PerformFilter(filterParameters)
                .OrderBy(e => e.DateTime)
                .ToListAsync();
            var total = result.Count;

            return ListResult<EquipmentOrder>.Success(result, total);
        }

        public async Task<EquipmentOrder?> Get(
            string id, 
            EquipmentOrderIncludeParameters includeParameters)
        {
            using var context = _dbFactory.CreateDbContext();         

            var equipmentOrder = await context.EquipmentOrders
                .AsNoTracking()
                .PerformInclude(includeParameters)
                .FirstOrDefaultAsync(m => m.Id == id);
            return equipmentOrder;
        }

        public async Task<string> Create(EquipmentOrder equipmentOrder)
        {
            using var context = _dbFactory.CreateDbContext();
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

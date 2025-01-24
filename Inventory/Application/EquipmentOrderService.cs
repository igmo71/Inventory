﻿using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IEquipmentOrderService
    {
        Task<ListResult<EquipmentOrder>> GetList(
            GridItemsProviderRequest<EquipmentOrder> request,
            EquipmentOrderIncludeParameters includeParameters,
            EquipmentOrderFilterParameters filterParameters);

        Task<EquipmentOrder?> Get(string id,
            bool isIncludeEquipment = false,
            bool isIncludeSerialNumber = false,
            bool isIncludeAuthor = false,
            bool isIncludeAssignee = false,
            bool isIncludeLocation = false);
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
            var query = context.EquipmentOrders.AsNoTracking();

            if (request.StartIndex > 0)
                query = query.Skip(request.StartIndex);

            if (request.Count is not null)
                query = query.Take((int)request.Count);            

            var result = await query
                .PerformInclude(includeParameters)
                .PerformFilter(filterParameters)
                .OrderBy(e => e.DateTime)
                .ToListAsync();
            var total = result.Count;

            return ListResult<EquipmentOrder>.Success(result, total);
        }

        public async Task<EquipmentOrder?> Get(string id, 
            bool isIncludeEquipment = false,
            bool isIncludeSerialNumber = false,
            bool isIncludeAuthor = false,
            bool isIncludeAssignee = false,
            bool isIncludeLocation = false)
        {
            using var context = _dbFactory.CreateDbContext();

            var query = context.EquipmentOrders.AsNoTracking();

            if (isIncludeEquipment)
                query = query.Include(e => e.Equipment);

            if (isIncludeSerialNumber)
                query = query.Include(e => e.SerialNumber);

            if (isIncludeAuthor)
                query = query.Include(e => e.Author);

            if (isIncludeAssignee)
                query = query.Include(e => e.Assignee);

            if (isIncludeLocation)
                query = query.Include(e => e.Location);

            var equipmentOrder = await query.FirstOrDefaultAsync(m => m.Id == id);
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

using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IEquipmentHistoryService
    {
        Task<ListResult<EquipmentHistory>> GetList(int? skip = null, int? take = null,
            bool isIncludeEquipment = false,
            bool isIncludeSerialNumber = false,
            bool isIncludeAssignee = false,
            bool isIncludeLocation = false);

        Task<EquipmentHistory?> Get(string id);
        Task<string> Create(EquipmentHistory equipmentHistory);
        Task<Result> Update(EquipmentHistory equipmentHistory);
        Task Delete(EquipmentHistory equipmentHistory);
        bool Exists(string id);
    }

    public class EquipmentHistoryService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEquipmentHistoryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<EquipmentHistory>> GetList(int? skip = null, int? take = null,
            bool isIncludeEquipment = false,
            bool isIncludeSerialNumber = false,
            bool isIncludeAssignee = false,
            bool isIncludeLocation = false)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.EquipmentHistories.AsNoTracking();

            if (skip is not null)
                query = query.Skip((int)skip);

            if (take is not null)
                query = query.Take((int)take);
            
            if (isIncludeEquipment)
                query = query.Include(e => e.Equipment);

            if (isIncludeSerialNumber)
                query = query.Include(e => e.SerialNumber);

            if (isIncludeAssignee)
                query = query.Include(e => e.Assignee);

            if (isIncludeLocation)
                query = query.Include(e => e.Location);

            var result = await query.ToListAsync();
            var total = context.Locations.Count();

            return ListResult<EquipmentHistory>.Success(result, total);
        }

        public async Task<EquipmentHistory?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var equipmentHistory = await context.EquipmentHistories.FirstOrDefaultAsync(m => m.Id == id);
            return equipmentHistory;
        }

        public async Task<string> Create(EquipmentHistory equipmentHistory)
        {
            using var context = _dbFactory.CreateDbContext();
            equipmentHistory.Id = Guid.CreateVersion7().ToString();
            context.EquipmentHistories.Add(equipmentHistory);
            await context.SaveChangesAsync();
            return equipmentHistory.Id;
        }

        public async Task<Result> Update(EquipmentHistory equipmentHistory)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(equipmentHistory).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(equipmentHistory.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(EquipmentHistory), equipmentHistory.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(EquipmentHistory equipmentHistory)
        {
            using var context = _dbFactory.CreateDbContext();
            context.EquipmentHistories.Remove(equipmentHistory);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.EquipmentHistories.Any(e => e.Id == id);
        }
    }
}

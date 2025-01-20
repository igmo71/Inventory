using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface ISerialNumberService
    {
        Task<ListResult<SerialNumber>> GetList(int? skip = null, int? take = null, 
            bool isIncludeEquipment = false, 
            string? equipmentId = null,
            bool isNotAssignedOnly = false);
        Task<SerialNumber?> Get(string id,
            bool isIncludeEquipment = false);
        Task<string> Create(SerialNumber serialNumber);
        Task<Result> Update(SerialNumber serialNumber);
        Task Delete(SerialNumber serialNumber);
        bool Exists(string id);
    }

    public class SerialNumberService(IDbContextFactory<ApplicationDbContext> dbFactory) : ISerialNumberService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<SerialNumber>> GetList(int? skip = null, int? take = null, 
            bool isIncludeEquipment = false, 
            string? equipmentId = null,
            bool isNotAssignedOnly = false)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.SerialNumbers.AsNoTracking();


            if (skip is not null)
                query = query.Skip((int)skip);

            if (take is not null)
                query = query.Take((int)take);

            if (isIncludeEquipment)
                query = query.Include(e => e.Equipment)
                    .Where(e => e.Equipment != null)
                    .OrderBy(e => e.Equipment!.Name)
                    .ThenBy(e => e.Number);

            if (equipmentId is not null)
                query = query.Where(e => e.EquipmentId == equipmentId);

            if (isNotAssignedOnly)
                query = query.Where(e => !e.IsAssigned);

            var result = await query.ToListAsync();
            var total = context.SerialNumbers.Count();

            return ListResult<SerialNumber>.Success(result, total);
        }

        public async Task<SerialNumber?> Get(string id,
            bool isIncludeEquipment = false)
        {
            using var context = _dbFactory.CreateDbContext();

            var query = context.SerialNumbers.AsNoTracking();

            if (isIncludeEquipment)
                query = query.Include(e => e.Equipment);

            var serialNumber = await query.FirstOrDefaultAsync(m => m.Id == id);
                return serialNumber;
        }

        public async Task<string> Create(SerialNumber serialNumber)
        {
            using var context = _dbFactory.CreateDbContext();
            serialNumber.Id = Guid.CreateVersion7().ToString();
            context.SerialNumbers.Add(serialNumber);
            await context.SaveChangesAsync();
            return serialNumber.Id;
        }

        public async Task<Result> Update(SerialNumber serialNumber)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(serialNumber).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(serialNumber.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(SerialNumber), serialNumber.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(SerialNumber serialNumber)
        {
            using var context = _dbFactory.CreateDbContext();
            context.SerialNumbers.Remove(serialNumber);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.SerialNumbers.Any(e => e.Id == id);
        }
    }
}

using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface ISerialNumberService
    {
        Task<ListResult<SerialNumber>> GetList(int skip, int? take);
        Task<SerialNumber?> Get(string id);
        Task<string> Create(SerialNumber serialNumber);
        Task<Result> Update(SerialNumber serialNumber);
        Task Delete(SerialNumber serialNumber);
        bool Exists(string id);
    }

    public class SerialNumberService(IDbContextFactory<ApplicationDbContext> dbFactory) : ISerialNumberService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<SerialNumber>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.SerialNumbers.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.SerialNumbers.Count();

            return ListResult<SerialNumber>.Success(result, total);
        }
     
        public async Task<SerialNumber?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var serialNumber = await context.SerialNumbers.FirstOrDefaultAsync(m => m.Id == id);
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

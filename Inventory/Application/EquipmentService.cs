using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IEquipmentService
    {
        Task<ListResult<Equipment>> GetList(int skip, int? take);
        Task<Domain.Equipment?> Get(string id);
        Task<string> Create(Domain.Equipment equipment);
        Task<Result> Update(Domain.Equipment equipment);
        Task Delete(Domain.Equipment equipment);
        bool Exists(string id);
    }

    public class EquipmentService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEquipmentService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<Domain.Equipment>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Equipments.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.Equipments.Count();

            return ListResult<Domain.Equipment>.Success(result, total);
        }

        public async Task<Domain.Equipment?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var equipment = await context.Equipments.FirstOrDefaultAsync(m => m.Id == id);
            return equipment;
        }

        public async Task<string> Create(Domain.Equipment equipment)
        {
            using var context = _dbFactory.CreateDbContext();
            equipment.Id = Guid.CreateVersion7().ToString();
            context.Equipments.Add(equipment);
            await context.SaveChangesAsync();
            return equipment.Id;
        }

        public async Task<Result> Update(Domain.Equipment equipment)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(equipment).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(equipment.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(Domain.Equipment), equipment.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Domain.Equipment equipment)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Equipments.Remove(equipment);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Equipments.Any(e => e.Id == id);
        }
    }
}

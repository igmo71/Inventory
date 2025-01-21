using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IEquipmentService
    {
        Task<ListResult<Equipment>> GetList(int? skip = null, int? take = null, 
            bool isIncludeParent = false, 
            bool isIncludeChildren = false,
            string? parentId = null);
        Task<List<Equipment>> GetListWithoutFolders();
        Task<List<Equipment>> GetFolders();
        Task<List<Equipment>> GetListWithoutParents();
        Task<List<Equipment>> GetChildren(string id);
        Task<Equipment?> Get(string id, bool isIncludeParent = false);
        Task<string> Create(Equipment equipment);
        Task<Result> Update(Equipment equipment);
        Task Delete(Equipment equipment);
        bool Exists(string id);
    }

    public class EquipmentService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEquipmentService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<Equipment>> GetList(int? skip = null, int? take = null, 
            bool isIncludeParent = false, 
            bool isIncludeChildren = false,
            string? parentId = null)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Equipment.AsNoTracking();

            if (skip is not null)
                query = query.Skip((int)skip);

            if (take is not null)
                query = query.Take((int)take);

            if (isIncludeParent)
                query = query.Include(e => e.Parent);

            if (isIncludeChildren)
                query = query.Include(e => e.Children);

            if(parentId != null)
                query = query.Where(e => e.ParentId == parentId);

            var result = await query.ToListAsync();
            var total = context.Equipment.Count();

            return ListResult<Equipment>.Success(result, total);
        }

        public async Task<List<Equipment>> GetListWithoutFolders()
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.Equipment.AsNoTracking()
                .Where(e => !e.IsFolder).ToListAsync();

            return result;
        }

        public async Task<List<Equipment>> GetFolders()
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.Equipment.AsNoTracking()
                .Where(e => e.IsFolder).ToListAsync();

            return result;
        }

        public async Task<List<Equipment>> GetListWithoutParents()
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.Equipment.AsNoTracking()
                .Where(e => e.ParentId == null).ToListAsync();

            return result;
        }

        public async Task<List<Equipment>> GetChildren(string id)
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.Equipment.AsNoTracking()
                .Where(e => e.ParentId == id).ToListAsync();

            return result;
        }

        public async Task<Equipment?> Get(string id, bool isIncludeParent = false)
        {
            using var context = _dbFactory.CreateDbContext();
            
            var query = context.Equipment.AsNoTracking();
            
            if(isIncludeParent)
                query = query.Include(e => e.Parent);
            
            var equipment = await query.FirstOrDefaultAsync(m => m.Id == id);
            
            return equipment;
        }

        public async Task<string> Create(Equipment equipment)
        {
            using var context = _dbFactory.CreateDbContext();
            equipment.Id = Guid.CreateVersion7().ToString();
            context.Equipment.Add(equipment);
            await context.SaveChangesAsync();
            return equipment.Id;
        }

        public async Task<Result> Update(Equipment equipment)
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
                    return Result.Fail(new NotFoundException(nameof(Equipment), equipment.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Equipment equipment)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Equipment.Remove(equipment);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Equipment.Any(e => e.Id == id);
        }
    }
}

using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Asset = Inventory.Domain.Asset;

namespace Inventory.Application
{
    public interface IAssetService
    {
        Task<ListResult<Asset>> GetList(int skip, int? take);
        Task<Asset?> Get(string id);
        Task<string> Create(Asset asset);
        Task<Result> Update(Asset asset);
        Task Delete(Asset asset);
        bool Exists(string id);
    }

    public class AssetService(IDbContextFactory<ApplicationDbContext> dbFactory) : IAssetService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<Asset>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Assets.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.Assets.Count();

            return ListResult<Asset>.Success(result, total);
        }

        public async Task<Asset?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var asset = await context.Assets.FirstOrDefaultAsync(m => m.Id == id);
            return asset;
        }

        public async Task<string> Create(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            asset.Id = Guid.CreateVersion7().ToString();
            context.Assets.Add(asset);
            await context.SaveChangesAsync();
            return asset.Id;
        }

        public async Task<Result> Update(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(asset).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(asset.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(Asset), asset.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Assets.Remove(asset);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Assets.Any(e => e.Id == id);
        }
    }
}

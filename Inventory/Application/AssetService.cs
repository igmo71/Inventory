using Inventory.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Asset = Inventory.Domain.Asset;

namespace Inventory.Application
{
    public interface IAssetService
    {
        Task<string> Create(Asset asset);
        Task<Asset?> Get(string id);
        Task Delete(Asset asset);
        Task Update(Asset asset);
        bool Exists(string id);
    }

    public class AssetService(IDbContextFactory<ApplicationDbContext> dbFactory) : IAssetService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<string> Create(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            asset.Id = Guid.CreateVersion7().ToString();
            context.Assets.Add(asset);
            await context.SaveChangesAsync();
            return asset.Id;
        }

        public async Task<Asset?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var asset = await context.Assets.FirstOrDefaultAsync(m => m.Id == id);
            return asset;
        }

        public async Task Delete(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Assets.Remove(asset);
            await context.SaveChangesAsync();
        }

        public async Task Update(Asset asset)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(asset).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(asset.Id!))
                {
                    throw; // TODO: Handle DbUpdateConcurrencyException
                }
                else
                {
                    throw;
                }
            }
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Assets.Any(e => e.Id == id);
        }
    }
}

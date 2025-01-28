using Azure.Core;
using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.LocationServices
{
    public interface ILocationService
    {
        Task<ListResult<Location>> GetList(GridItemsProviderRequest<Location> request, bool isIncludeParent = false);
        Task<List<Location>> GetList(bool isIncludeParent = false);
        Task<Location?> Get(string id, bool isIncludeParent = false);
        Task<string> Create(Location location);
        Task<Result> Update(Location location);
        Task Delete(Location location);
        bool Exists(string id);
    }

    public class LocationService(IDbContextFactory<ApplicationDbContext> dbFactory) : ILocationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<Location>> GetList(GridItemsProviderRequest<Location> request, bool isIncludeParent = false)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Locations.AsNoTracking();

            query = query.HandleRequest(request);

            if (isIncludeParent)
                query = query.Include(e => e.Parent);

            var result = await query.ToListAsync();
            var total = context.Locations.Count();

            return ListResult<Location>.Success(result, total);
        }

        public async Task<List<Location>> GetList(bool isIncludeParent = false)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Locations.AsNoTracking();

            if (isIncludeParent)
                query = query.Include(e => e.Parent);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<Location?> Get(string id, bool isIncludeParent = false)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Locations.AsNoTracking();

            if (isIncludeParent)
                query = query.Include(e => e.Parent);

            var location = await query.FirstOrDefaultAsync(m => m.Id == id);

            return location;
        }

        public async Task<string> Create(Location location)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Locations.Add(location);
            await context.SaveChangesAsync();
            return location.Id;
        }

        public async Task<Result> Update(Location location)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(location).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(location.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(Location), location.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Location location)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Locations.Remove(location);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Locations.Any(e => e.Id == id);
        }
    }
}

using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IStockTurnoverService
    {
        Task<ListResult<StockTurnover>> GetList(int skip, int? take);
        Task<StockTurnover?> Get(string id);
        Task<string> Create(StockTurnover stockTurnover);
        Task<Result> Update(StockTurnover stockTurnover);
        Task Delete(StockTurnover stockTurnover);
        bool Exists(string id);
    }

    public class StockTurnoverService(IDbContextFactory<ApplicationDbContext> dbFactory) : IStockTurnoverService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<StockTurnover>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.StockTurnovers.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.Orders.Count();

            return ListResult<StockTurnover>.Success(result, total);
        }

        public async Task<StockTurnover?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var stockTurnover = await context.StockTurnovers.FirstOrDefaultAsync(m => m.Id == id);
            return stockTurnover;
        }

        public async Task<string> Create(StockTurnover stockTurnover)
        {
            using var context = _dbFactory.CreateDbContext();
            stockTurnover.Id = Guid.CreateVersion7().ToString();
            context.StockTurnovers.Add(stockTurnover);
            await context.SaveChangesAsync();
            return stockTurnover.Id;
        }

        public async Task<Result> Update(StockTurnover stockTurnover)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(stockTurnover).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(stockTurnover.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(StockTurnover), stockTurnover.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(StockTurnover stockTurnover)
        {
            using var context = _dbFactory.CreateDbContext();
            context.StockTurnovers.Remove(stockTurnover);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.StockTurnovers.Any(e => e.Id == id);
        }
    }
}

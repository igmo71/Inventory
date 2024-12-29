using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IStockBalanceService
    {
        Task<ListResult<StockBalance>> GetList(int skip, int? take);
        Task<StockBalance?> Get(string id);
        Task<string> Create(StockBalance stockBalance);
        Task<Result> Update(StockBalance stockBalance);
        Task Delete(StockBalance stockBalance);
        bool Exists(string id);
    }

    public class StockBalanceService(IDbContextFactory<ApplicationDbContext> dbFactory) : IStockBalanceService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<StockBalance>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.StockBalances.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.Orders.Count();

            return ListResult<StockBalance>.Success(result, total);
        }

        public async Task<StockBalance?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var order = await context.StockBalances.FirstOrDefaultAsync(m => m.Id == id);
            return order;
        }

        public async Task<string> Create(StockBalance stockBalance)
        {
            using var context = _dbFactory.CreateDbContext();
            stockBalance.Id = Guid.CreateVersion7().ToString();
            context.StockBalances.Add(stockBalance);
            await context.SaveChangesAsync();
            return stockBalance.Id;
        }

        public async Task<Result> Update(StockBalance stockBalance)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(stockBalance).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(stockBalance.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(StockBalance), stockBalance.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(StockBalance stockBalance)
        {
            using var context = _dbFactory.CreateDbContext();
            context.StockBalances.Remove(stockBalance);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.StockBalances.Any(e => e.Id == id);
        }
    }
}

using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Inventory.Application
{
    public interface IOrderService
    {
        Task<ListResult<Order>> GetList(int skip, int? take);
        Task<Order?> Get(string id);
        Task<string> Create(Order order);
        Task<Result> Update(Order order);
        Task Delete(Order order);
        bool Exists(string id);
    }

    public class OrderService(IDbContextFactory<ApplicationDbContext> dbFactory) : IOrderService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<Order>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Orders.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.Orders.Count();

            return ListResult<Order>.Success(result, total);
        }

        public async Task<Order?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var order = await context.Orders.FirstOrDefaultAsync(m => m.Id == id);
            return order;
        }

        public async Task<string> Create(Order order)
        {
            using var context = _dbFactory.CreateDbContext();
            order.Id = Guid.CreateVersion7().ToString();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Result> Update(Order order)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(order).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(order.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(Order), order.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Order order)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Orders.Any(e => e.Id == id);
        }
    }
}

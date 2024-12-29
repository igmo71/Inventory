using Inventory.Common;
using Inventory.Common.Results;
using Inventory.Data;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application
{
    public interface IOrderItemService
    {
        Task<ListResult<OrderItem>> GetList(int skip, int? take);
        Task<OrderItem?> Get(string id);
        Task<string> Create(OrderItem orderItem);
        Task<Result> Update(OrderItem orderItem);
        Task Delete(OrderItem orderItem);
        bool Exists(string id);
    }

    public class OrderItemService(IDbContextFactory<ApplicationDbContext> dbFactory) : IOrderItemService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        public async Task<ListResult<OrderItem>> GetList(int skip, int? take)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.OrderItems.Skip(skip);

            if (take is not null)
                query = query.Take((int)take);

            var result = await query.AsNoTracking().ToListAsync();
            var total = context.OrderItems.Count();

            return ListResult<OrderItem>.Success(result, total);
        }

        public async Task<OrderItem?> Get(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            var orderItem = await context.OrderItems.FirstOrDefaultAsync(m => m.Id == id);
            return orderItem;
        }

        public async Task<string> Create(OrderItem orderItem)
        {
            using var context = _dbFactory.CreateDbContext();
            orderItem.Id = Guid.CreateVersion7().ToString();
            context.OrderItems.Add(orderItem);
            await context.SaveChangesAsync();
            return orderItem.Id;
        }

        public async Task<Result> Update(OrderItem orderItem)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Attach(orderItem).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(orderItem.Id!))
                {
                    return Result.Fail(new NotFoundException(nameof(OrderItem), orderItem.Id));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(OrderItem orderItem)
        {
            using var context = _dbFactory.CreateDbContext();
            context.OrderItems.Remove(orderItem);
            await context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            using var context = _dbFactory.CreateDbContext();
            return context.OrderItems.Any(e => e.Id == id);
        }
    }
}

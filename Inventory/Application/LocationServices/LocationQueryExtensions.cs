using Inventory.Domain;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace Inventory.Application.LocationServices
{
    public static class LocationQueryExtensions
    {
        public static IQueryable<Location> HandleRequest(this IQueryable<Location> query, GridItemsProviderRequest<Location> request)
        {
            if (request.StartIndex > 0)
                query = query.Skip(request.StartIndex);

            if (request.Count is not null)
                query = query.Take((int)request.Count);

            return query;
        }
    }
}

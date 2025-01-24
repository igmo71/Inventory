using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.EquipmentOrderServices
{
    public static class EquipmentOrderQueryExtensions
    {
        public static IQueryable<EquipmentOrder> PerformInclude(this IQueryable<EquipmentOrder> query, EquipmentOrderIncludeParameters includeParameters)
        {
            if (includeParameters.isIncludeEquipment)
                query = query.Include(e => e.Equipment);

            if (includeParameters.isIncludeSerialNumber)
                query = query.Include(e => e.SerialNumber);

            if (includeParameters.isIncludeAuthor)
                query = query.Include(e => e.Author);

            if (includeParameters.isIncludeAssignee)
                query = query.Include(e => e.Assignee);

            if (includeParameters.isIncludeLocation)
                query = query.Include(e => e.Location);

            return query;
        }

        public static IQueryable<EquipmentOrder> PerformFilter(this IQueryable<EquipmentOrder> query, EquipmentOrderFilterParameters filterParameters)
        {
            if (filterParameters.equipment is not null)
                query = query.Where(e =>
                    e.Equipment != null &&
                    e.Equipment.Name != null &&
                    e.Equipment.Name.Contains(filterParameters.equipment));

            if (filterParameters.serialNumber is not null)
                query = query.Where(e =>
                    e.SerialNumber != null &&
                    e.SerialNumber.Number != null &&
                    e.SerialNumber.Number.Contains(filterParameters.serialNumber));

            if (filterParameters.assignee is not null)
                query = query.Where(e =>
                    e.Assignee != null &&
                    e.Assignee.Name != null &&
                    (e.Assignee.Name.Contains(filterParameters.assignee) || e.Assignee.Id == filterParameters.assignee));

            if (filterParameters.location is not null)
                query = query.Where(e =>
                    e.Location != null &&
                    e.Location.Name != null &&
                    e.Location.Name.Contains(filterParameters.location));

            return query;
        }
    }
}

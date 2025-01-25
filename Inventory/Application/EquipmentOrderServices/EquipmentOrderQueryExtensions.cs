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
            if (filterParameters.equipmentId is not null)
                query = query.Where(e =>
                    e.Equipment != null &&
                    e.Equipment.Id != null &&
                    e.Equipment.Id == filterParameters.equipmentId);

            if (filterParameters.equipmentName is not null)
                query = query.Where(e =>
                    e.Equipment != null &&
                    e.Equipment.Name != null &&
                    e.Equipment.Name.Contains(filterParameters.equipmentName));

            if (filterParameters.serialNumberId is not null)
                query = query.Where(e =>
                    e.SerialNumber != null &&
                    e.SerialNumber.Id != null &&
                    e.SerialNumber.Id == filterParameters.serialNumberId);

            if (filterParameters.serialNumber is not null)
                query = query.Where(e =>
                    e.SerialNumber != null &&
                    e.SerialNumber.Number != null &&
                    e.SerialNumber.Number.Contains(filterParameters.serialNumber));

            if (filterParameters.assigneeId is not null)
                query = query.Where(e =>
                    e.Assignee != null &&
                    e.Assignee.Id != null &&
                    e.Assignee.Id == filterParameters.assigneeId);

            if (filterParameters.assigneeName is not null)
                query = query.Where(e =>
                    e.Assignee != null &&
                    e.Assignee.Name != null &&
                    (e.Assignee.Name.Contains(filterParameters.assigneeName) || e.Assignee.Id == filterParameters.assigneeName));


            if (filterParameters.locationId is not null)
                query = query.Where(e =>
                    e.Location != null &&
                    e.Location.Id != null &&
                    e.Location.Id == filterParameters.locationId);

            if (filterParameters.locationName is not null)
                query = query.Where(e =>
                    e.Location != null &&
                    e.Location.Name != null &&
                    e.Location.Name.Contains(filterParameters.locationName));

            return query;
        }
    }
}

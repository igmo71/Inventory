﻿using Inventory.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Number { get; set; }

        public OrderStatus? Status { get; set; }
        public Guid StatusId { get; set; }

        public OrderDirection? Direction { get; set; }
        public Guid DirectionId { get; set; }

        public ApplicationUser? Author { get; set; }
        public string? AuthorId { get; set; }

        public ApplicationUser? Assignee { get; set; }
        public string? AssigneeId { get; set; }

        public Location? LocationFrom { get; set; }
        public Guid? LocationFromId { get; set; }

        public Location? LocationTo { get; set; }
        public Guid? LocationToId { get; set; }

        public List<OrderItem>? Items { get; set; }

        [NotMapped]
        public string? StatusIdString
        {
            get
            {
                return StatusId.ToString();
            }
            set
            {
                if (value != null)
                    StatusId = Guid.Parse(value);
            }
        }

        [NotMapped]
        public string? DirectionIdString
        {
            get
            {
                return DirectionId.ToString();
            }
            set
            {
                if (value != null)
                    DirectionId = Guid.Parse(value);
            }
        }

        [NotMapped]
        public string? LocationFromIdString
        {
            get
            {
                return LocationFromId.ToString();
            }
            set
            {
                if (value != null)
                    LocationFromId = Guid.Parse(value);
            }
        }

        [NotMapped]
        public string? LocationToIdString
        {
            get
            {
                return LocationToId.ToString();
            }
            set
            {
                if (value != null)
                    LocationToId = Guid.Parse(value);
            }
        }
    }
}

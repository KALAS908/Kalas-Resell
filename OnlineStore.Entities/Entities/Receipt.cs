﻿#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Receipt : IEntity
    { 
        public Receipt()
        {
            OrderedItems = new HashSet<OrderedItems>();
        }

        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public double? TotalPrice { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }
    }
}
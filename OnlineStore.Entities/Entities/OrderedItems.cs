#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class OrderedItems : IEntity
    {
        public int OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double? Quantity { get; set; }

        public virtual Receipt Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
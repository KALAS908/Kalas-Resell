#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Goods : IEntity
    {
        public int Id { get; set; }
        public int? ProviderId { get; set; }
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
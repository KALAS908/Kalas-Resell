#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Provider : IEntity
    {
        public Provider()
        {
            Goods = new HashSet<Goods>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }
    }
}
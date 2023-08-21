#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Category : IEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
            Brand = new HashSet<Brand>();
        }

        public int Id { get; set; }
        public int? GenderId { get; set; }
        public int? TypeId { get; set; }
        public string Name { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Product> Product { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }
    }
}
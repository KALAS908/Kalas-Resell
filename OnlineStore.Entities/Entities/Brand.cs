﻿#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Brand :IEntity
    {
        public Brand()
        {
            Product = new HashSet<Product>();
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
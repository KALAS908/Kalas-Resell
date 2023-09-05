﻿#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Measure : IEntity
    {
        public Measure()
        {
            ProductMeasure = new HashSet<ProductMeasure>();
            ShoppingCart = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public string MeasureValue { get; set; }
        public int? TypeId { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<ProductMeasure> ProductMeasure { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
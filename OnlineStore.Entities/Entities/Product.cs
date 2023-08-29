#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Product : IEntity
    {
        public Product()
        {
            Goods = new HashSet<Goods>();
            Image = new HashSet<Image>();
            OrderedItems = new HashSet<OrderedItems>();
            ProductMeasure = new HashSet<ProductMeasure>();
            ShoppingCart = new HashSet<ShoppingCart>();
            WishList = new HashSet<WishList>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int ColorId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<Goods> Goods { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<OrderedItems> OrderedItems { get; set; }
        public virtual ICollection<ProductMeasure> ProductMeasure { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
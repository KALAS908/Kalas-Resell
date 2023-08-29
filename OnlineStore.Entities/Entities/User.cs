#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class User : IEntity
    {
        public User()
        {
            Goods = new HashSet<Goods>();
            Receipt = new HashSet<Receipt>();
            ShoppingCart = new HashSet<ShoppingCart>();
            UserString = new HashSet<UserString>();
            WishList = new HashSet<WishList>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }

        public virtual Country Country { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Goods> Goods { get; set; }
        public virtual ICollection<Receipt> Receipt { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
        public virtual ICollection<UserString> UserString { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class WishList : IEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
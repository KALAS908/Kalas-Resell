﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Image : IEntity
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
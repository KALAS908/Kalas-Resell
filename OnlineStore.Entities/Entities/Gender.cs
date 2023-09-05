﻿#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Gender : IEntity
    {
        public Gender()
        {
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
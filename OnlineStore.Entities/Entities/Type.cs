#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class Type : IEntity
    {
        public Type()
        {
            Category = new HashSet<Category>();
            Measure = new HashSet<Measure>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Measure> Measure { get; set; }
    }
}
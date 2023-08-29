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
            Measure = new HashSet<Measure>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Measure> Measure { get; set; }
    }
}
#nullable disable
using OnlineStore.Common;
using System;
using System.Collections.Generic;

namespace OnlineStore.Entities.Entities
{
    public partial class UserString : IEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string RandomString { get; set; }

        public virtual User User { get; set; }
    }
}
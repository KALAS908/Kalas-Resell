using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
    public class OrderedItemDto
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

    }
}

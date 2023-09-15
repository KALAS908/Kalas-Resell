using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
     public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
    }
}

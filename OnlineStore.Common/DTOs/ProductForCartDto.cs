using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
    public  class ProductForCartDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int MeasureId { get; set; }
        public byte[] CoverImage { get; set; }
    }
}

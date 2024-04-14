using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
    public class ProductWithMeasureDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Discount { get; set; }
        public int MeasureId { get; set; }
        public string Color { get; set; }
        public int TypeId { get; set; }
        public byte[] CoverImage { get; set; }
        public List<QuantityMeasureDto> QuantityMeasures { get; set; }

        public List<byte[]> Images { get; set; }
        public List<IFormFile> FileImages { get; set; }
    }
}

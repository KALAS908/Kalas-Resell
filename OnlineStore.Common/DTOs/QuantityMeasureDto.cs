using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
    public class QuantityMeasureDto
    {
        public int MeasureId { get; set; }
        public string MeasureName { get; set; }
        public int Quantity { get; set; }
    }
}

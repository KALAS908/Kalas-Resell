using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common.DTOs
{
    public class MeasureQuantityDto
    {
        int MeasureId { get; set; }
        int Quantity { get; set; }
        Guid ProductId { get; set; }
    }
}

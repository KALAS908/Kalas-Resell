using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Products.Models
{
    public class MeasureQuantityModel
    {
        public string ProductName { get; set; }
        public int MeasureId { get; set; }

        public int TypeId { get; set; }

        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
